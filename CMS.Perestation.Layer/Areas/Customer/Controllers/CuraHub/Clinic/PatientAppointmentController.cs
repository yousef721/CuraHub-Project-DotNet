using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub.IdentitySection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CMS.Perestation.Layer.Areas.Customer.Controllers.CuraHub.Clinic
{
    [Area(nameof(Customer))]
    [Route("Customer/CuraHub/Clinic/PatientAppointment")]
    public class PatientAppointmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        // Customer/CuraHub/Clinic/PatientAppointment/Index
        public PatientAppointmentController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            this._unitOfWork = unitOfWork;
            this._userManager = userManager;
        }
        [Route("Index")]
        public IActionResult Index(DateOnly date, int DoctorId ,int dateIndex)
        {
            date = DateOnly.FromDateTime(DateTime.Now.AddDays(dateIndex));
            //var patients = this._unitOfWork.PatientRepository.Retrive().Include(e => e.PatientAppointments);
            var appointments = this._unitOfWork.PatientAppointmentRepository.Retrive(filter: e => e.date == date && e.Schedule.DoctorId == DoctorId,
                includeProps: [e => e.Patient]
                ).Include(e => e.Schedule).ThenInclude(e => e.Doctor).ToList();

            TempData["returnUrl"] = $"https://localhost:7295/Customer/CuraHub/Clinic/PatientAppointment/Index?date={date}&DoctorId={DoctorId}";

            return View(appointments);
        }
        //Customer/CuraHub/Clinic/PatientAppointment/UserAppoitments?AppicationUserId=
        [Route("UserAppoitments")]
        public IActionResult UserAppoitments(string? AppicationUserId )
        {

            AppicationUserId = _userManager.GetUserId(User);
            var appointments = this._unitOfWork.PatientAppointmentRepository.Retrive(filter: e => e.Patient.ApplicationUserId == AppicationUserId
            , includeProps: [e => e.Patient]).Include(e => e.Schedule).ThenInclude(e => e.Doctor).ToList();


            foreach (var appointment in appointments)
            {
                var nowTime = DateOnly.FromDateTime(DateTime.Now);
                var date =  appointment.date;

                if (date < nowTime)
                {
                    appointment.Schedule.Available = true;
                    if (date.AddDays(3) < nowTime)
                    {
                        _unitOfWork.PatientAppointmentRepository.Delete(appointment);
                    }
                }
                var medicalPrescriptions = _unitOfWork.MedicalPrescriptionRepository.Retrive(filter: e => e.PatientId == appointment.PatientId
                && e.ScheduleId == appointment.ScheduleId && e.Date == appointment.date).ToList();
                appointment.MedicalPrescriptions = medicalPrescriptions;
            }
            _unitOfWork.Commit();

            return View(appointments);
        }
        // Customer/CuraHub/Clinic/PatientAppointment/ShowAppoitments
        [Route("ShowAppoitments")]
        public IActionResult ShowAppoitments(DateOnly date, int ClinicReceptionistId, int dateIndex)
        {
            date = DateOnly.FromDateTime(DateTime.Now.AddDays(dateIndex));

            TempData["ClinicReceptionistId"] = ClinicReceptionistId;
            var clinicReceptionist = this._unitOfWork.ClinicReceptionistRepository.RetriveItem(filter: e => e.Id == ClinicReceptionistId);
            if (clinicReceptionist != null)
            {
                var appointments = this._unitOfWork.PatientAppointmentRepository.Retrive(filter: e => e.date == date && e.Schedule.DoctorId == clinicReceptionist.DoctorId,
                   includeProps: [e => e.Patient]
                   ).Include(e => e.Schedule).ThenInclude(e => e.Doctor).ToList();
                return View(appointments);

            }
            return RedirectToAction("Index", "Home");
        }
    }
}
