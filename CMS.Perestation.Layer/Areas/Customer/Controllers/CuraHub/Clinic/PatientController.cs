using AutoMapper;
using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub.ClinicSection;
using CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.PatientVM;
using CMS.Models.CuraHub.IdentitySection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CMS.Perestation.Layer.Areas.Customer.Controllers.CuraHub.Clinic
{
    [Area(nameof(Customer))]
    [Route("Customer/CuraHub/Clinic/Patient")]
    public class PatientController : Controller
    {
        // Customer/CuraHub/Clinic/Patient/Index?date=02/02/2025&DoctorId=1 

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;


        public PatientController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._userManager = userManager;
        }


        [Route("Details")]
        public IActionResult Details(int PatientId)
        {
            var Patient = this._unitOfWork.PatientRepository.RetriveItem(filter: e =>e.Id == PatientId);
            if (Patient !=null)
            {
                return View(Patient);

            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("Upsert")]
        public IActionResult Upsert(PatientUpsartVM patientUpsartVM ,int ScheduleId , DateOnly date ,int dayIndex)
        {
            
            date = DateOnly.FromDateTime(DateTime.Now.AddDays(dayIndex-1));
            

            var user = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
            if (user != null)
            {
                patientUpsartVM.ApplicationUserId = user.Id;
                patientUpsartVM.FirstName = patientUpsartVM.FirstName??user.FirstName;
                patientUpsartVM.LastName = patientUpsartVM.LastName??user.LastName;
                patientUpsartVM.Email = user.Email??"@CuraHub.com";
                patientUpsartVM.ProfilePicture = patientUpsartVM.ProfilePicture??"PatientDefault.avif";
                var schedule = _unitOfWork.ScheduleRepository.RetriveItem(e => e.Id == ScheduleId);
               if(schedule !=null)
                {
                    patientUpsartVM.DoctorId = schedule.DoctorId;

                }
                patientUpsartVM.patientAppointment.ScheduleId = ScheduleId;
                patientUpsartVM.patientAppointment.date = date;
                return View(patientUpsartVM);

            }
            return RedirectToAction(actionName: "Login", controllerName: "Account", new { area = "Identity" });
        }
        [HttpPost]
        [Route("Upsert")]
        public IActionResult Upsert(PatientUpsartVM patientUpsartVM , PatientAppointment patientAppointment)
        {
            patientUpsartVM.patientAppointment = patientAppointment;
            ModelState.Remove("Schedule");
            ModelState.Remove("Patient");

            ModelState.Remove("patientAppointment.Schedule");
            ModelState.Remove("patientAppointment.Patient");

            patientUpsartVM.City = "Nasr City";

            if (ModelState.IsValid)
            {
                var patient = this._mapper.Map<Patient>(patientUpsartVM);
                var InputPatient = _unitOfWork.PatientRepository.RetriveItem(filter: e => e.PersonalNationalIDNumber == patient.PersonalNationalIDNumber);

                if (InputPatient != null)
                {
                    if(InputPatient.MedicalAnalysis != null && InputPatient.MedicalAnalysis != "MedicalAnalysis")
                    this._unitOfWork.PatientRepository.Update(patient);

                }
                else
                {
                    this._unitOfWork.PatientRepository.Create(patient);
                }


                this._unitOfWork.Commit();
                var savedPatient = this._unitOfWork.PatientRepository.RetriveItem(e =>e.PersonalNationalIDNumber ==  patient.PersonalNationalIDNumber);
                if (savedPatient != null) 
                {
                    patientAppointment.PatientId = savedPatient.Id;
                    this._unitOfWork.PatientAppointmentRepository.Create(patientAppointment);
                    var schedul = this._unitOfWork.ScheduleRepository.RetriveItem(e => e.Id == patientAppointment.ScheduleId);
                    if(schedul != null)
                    {
                        schedul.Available = false;
                        this._unitOfWork.ScheduleRepository.Update(schedul);

                    }

                    this._unitOfWork.Commit();
                    return RedirectToAction("PayOption", "PatientAppointmentCard", routeValues: patientAppointment);
                }
                return RedirectToAction("Index", "Home");
            }
            return View(patientUpsartVM);
        }


    }
}
