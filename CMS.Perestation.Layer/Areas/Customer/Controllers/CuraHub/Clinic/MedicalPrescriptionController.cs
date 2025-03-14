using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub.ClinicSection;
using CMS.Models.CuraHub.ClinicSection.ClinicSectionVM;
using CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.PatientVM;
using CMS.Models.CuraHub.IdentitySection;
using CMS.Utitlities.StaticData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CMS.Perestation.Layer.Areas.Customer.Controllers.CuraHub.Clinic
{
    [Area(nameof(Customer))]
    [Route("Customer/CuraHub/Clinic/MedicalPrescription")]
    public class MedicalPrescriptionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        // Customer/CuraHub/Clinic/PatientAppointment/Index
        public MedicalPrescriptionController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            this._unitOfWork = unitOfWork;
            this._userManager = userManager;
        }
        // Customer/CuraHub/Clinic/MedicalPrescription/Index?DoctorId=1,PatientId=1,Date=05/02/1998
        [Route("Index")]
        public IActionResult Index(int ScheduleId, int PatientId, DateOnly Date)
        {
            Date = DateOnly.FromDateTime(DateTime.Now);
            var medicalPrescriptions = this._unitOfWork.MedicalPrescriptionRepository.Retrive(filter: e => e.PatientId == PatientId && e.ScheduleId == ScheduleId && e.Date == Date, includeProps: [e => e.PatientAppointment]);
            Cust_MedicalPrescriptionsVM cust_MedicalPrescriptionsVM = new Cust_MedicalPrescriptionsVM();
            cust_MedicalPrescriptionsVM.MedicalPrescriptions = medicalPrescriptions.ToList();
            var schedule = _unitOfWork.ScheduleRepository.Retrive(filter: e =>e.Id == ScheduleId).Include(e =>e.Doctor).ThenInclude(e =>e.Specialization).FirstOrDefault() ;
            if(schedule !=null)
            {
                 cust_MedicalPrescriptionsVM.Schedule = schedule;

            }
           
            var patient = _unitOfWork.PatientRepository.RetriveItem(filter: e => e.Id == PatientId);
            if (patient != null)
            {
                cust_MedicalPrescriptionsVM.Patient = patient;
            } 
            cust_MedicalPrescriptionsVM.date = Date;
            return View(cust_MedicalPrescriptionsVM);
        }
        [HttpGet]
        [Route("Create")]
        //[Authorize(Roles = ($"{Role.DoctorRole}"))]
        // Customer/CuraHub/Clinic/MedicalPrescription/Create
        public IActionResult Create(int ScheduleId , int PatientId )
        {
            Doctor_MedicalPrescriptionCreateVM doctor_MedicalPrescriptionCreateVM = new Doctor_MedicalPrescriptionCreateVM();
            doctor_MedicalPrescriptionCreateVM.date = DateOnly.FromDateTime(DateTime.Now);
            doctor_MedicalPrescriptionCreateVM.PatientId = PatientId;
            doctor_MedicalPrescriptionCreateVM.ScheduleId = ScheduleId;
            var appointment = this._unitOfWork.PatientAppointmentRepository.Retrive(filter: e => e.ScheduleId == ScheduleId && e.PatientId == PatientId,
               includeProps: [e => e.Patient]
               ).Include(e => e.Schedule).ThenInclude(e => e.Doctor).ThenInclude(e =>e.Specialization).FirstOrDefault();
            if (appointment != null)
            {
                doctor_MedicalPrescriptionCreateVM.PatientAppointment = appointment;
                
            }
            return View(doctor_MedicalPrescriptionCreateVM);
        }
        [HttpPost]
        [Route("Create")]
        //[Authorize(Roles = ($"{Role.DoctorRole}"))]
        // Customer/CuraHub/Clinic/MedicalPrescription/Create
        public IActionResult Create(Doctor_MedicalPrescriptionCreateVM doctor_MedicalPrescriptionCreateVM)
        {
            
                if (doctor_MedicalPrescriptionCreateVM.numOfTaken1 !=0)
                {
                    var medicalPrescription = new MedicalPrescription()
                    {
                        MedicineType = doctor_MedicalPrescriptionCreateVM.MedicineType1,
                        numOfTaken = doctor_MedicalPrescriptionCreateVM.numOfTaken1,
                        ScheduleId = doctor_MedicalPrescriptionCreateVM.ScheduleId,
                        PatientId = doctor_MedicalPrescriptionCreateVM.PatientId,
                        Details = doctor_MedicalPrescriptionCreateVM.Details1,
                        Date = doctor_MedicalPrescriptionCreateVM.date
                    };
                    _unitOfWork.MedicalPrescriptionRepository.Create(medicalPrescription);
                }
                if (doctor_MedicalPrescriptionCreateVM.numOfTaken2 != 0)
                {
                    var medicalPrescription = new MedicalPrescription()
                    {
                        MedicineType = doctor_MedicalPrescriptionCreateVM.MedicineType2,
                        numOfTaken = doctor_MedicalPrescriptionCreateVM.numOfTaken2,
                        ScheduleId = doctor_MedicalPrescriptionCreateVM.ScheduleId,
                        PatientId = doctor_MedicalPrescriptionCreateVM.PatientId,
                        Details = doctor_MedicalPrescriptionCreateVM.Details2,
                        Date = doctor_MedicalPrescriptionCreateVM.date
                    };
                    _unitOfWork.MedicalPrescriptionRepository.Create(medicalPrescription);
                }
                if (doctor_MedicalPrescriptionCreateVM.numOfTaken3 != 0)
                {
                    var medicalPrescription = new MedicalPrescription()
                    {
                        MedicineType = doctor_MedicalPrescriptionCreateVM.MedicineType3,
                        numOfTaken = doctor_MedicalPrescriptionCreateVM.numOfTaken3,
                        ScheduleId = doctor_MedicalPrescriptionCreateVM.ScheduleId,
                        PatientId = doctor_MedicalPrescriptionCreateVM.PatientId,
                        Details = doctor_MedicalPrescriptionCreateVM.Details3,
                        Date = doctor_MedicalPrescriptionCreateVM.date
                    };
                    _unitOfWork.MedicalPrescriptionRepository.Create(medicalPrescription);
                }
                if (doctor_MedicalPrescriptionCreateVM.numOfTaken4 != 0)
                {
                    var medicalPrescription = new MedicalPrescription()
                    {
                        MedicineType = doctor_MedicalPrescriptionCreateVM.MedicineType4,
                        numOfTaken = doctor_MedicalPrescriptionCreateVM.numOfTaken4,
                        ScheduleId = doctor_MedicalPrescriptionCreateVM.ScheduleId,
                        PatientId = doctor_MedicalPrescriptionCreateVM.PatientId,
                        Details = doctor_MedicalPrescriptionCreateVM.Details4,
                        Date = doctor_MedicalPrescriptionCreateVM.date
                    };
                    _unitOfWork.MedicalPrescriptionRepository.Create(medicalPrescription);
                }
                if (doctor_MedicalPrescriptionCreateVM.numOfTaken5 != 0)
                {
                    var medicalPrescription = new MedicalPrescription()
                    {
                        MedicineType = doctor_MedicalPrescriptionCreateVM.MedicineType5,
                        numOfTaken = doctor_MedicalPrescriptionCreateVM.numOfTaken5,
                        ScheduleId = doctor_MedicalPrescriptionCreateVM.ScheduleId,
                        PatientId = doctor_MedicalPrescriptionCreateVM.PatientId,
                        Details = doctor_MedicalPrescriptionCreateVM.Details5,
                        Date = doctor_MedicalPrescriptionCreateVM.date
                    };
                    _unitOfWork.MedicalPrescriptionRepository.Create(medicalPrescription);
                }
                _unitOfWork.Commit();
                return RedirectToAction("Index", "MedicalPrescription" ,
                    routeValues: new { doctor_MedicalPrescriptionCreateVM.ScheduleId,
                                       doctor_MedicalPrescriptionCreateVM.PatientId,
                                       doctor_MedicalPrescriptionCreateVM.date
                                     });
           
        }
    }
}
