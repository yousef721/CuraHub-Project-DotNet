using AutoMapper;
using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub.ClinicSection;
using CMS.Models.CuraHub.ClinicSection.ClinicSectionVM;
using CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.ScheduleVM;
using CMS.Models.CuraHub.IdentitySection;
using CMS.Utitlities.StaticData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Perestation.Layer.Areas.Admin.Controllers.CuraHub.Clinic
{
    [Area(nameof(Admin))]
    [Authorize(Roles = ($"{Role.AdminRole}"))]
    [Route("Admin/CuraHub/Clinic/Schedule")]
    public class ScheduleController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ScheduleController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }



        [Route("Index")]
        public IActionResult Index(int? doctorId = null , int PageNumber = 1)
        {
            var Schedules = this._unitOfWork.ScheduleRepository.Retrive(includeProps: [e => e.Doctor]);

            SchedulesVM schedulesVM = new SchedulesVM();
            if (doctorId != null)
            {
                Schedules = Schedules.Where(e => e.DoctorId == doctorId);
            }
            if( PageNumber<1) { PageNumber =1; }
            schedulesVM.currentPageNumber = PageNumber;
            schedulesVM.TotalSchedulesCount = Schedules.Count();
            schedulesVM.Schedules = Schedules.ToList();
            schedulesVM.Doctors = this._unitOfWork.DoctorRepository.Retrive().ToList();
            return View(schedulesVM);
        }


        [Route("LockAvailable")]
        public IActionResult LockAvailable(int ScheduleId , int DoctorId)
        {
            var schedule = this._unitOfWork.ScheduleRepository.RetriveItem(filter: e => e.Id == ScheduleId);
            if (schedule != null)
            {
                schedule.Available = false;
                this._unitOfWork.ScheduleRepository.Update(schedule);
                this._unitOfWork.Commit();
            }
            TempData["DoctorId"] = DoctorId;

            return RedirectToAction("ShowDoctorSchedule", routeValues: DoctorId);
        }
        [Route("UnLockAvailable")]
        public IActionResult UnLockAvailable(int ScheduleId , int DoctorId)
        {
            var schedule = this._unitOfWork.ScheduleRepository.RetriveItem(filter: e => e.Id == ScheduleId);
            if (schedule != null)
            {
                schedule.Available = true;
                this._unitOfWork.ScheduleRepository.Update(schedule);
                this._unitOfWork.Commit();
            }
            TempData["DoctorId"] = DoctorId;
            return RedirectToAction("ShowDoctorSchedule" , routeValues: DoctorId);
        }

        [Route("ShowDoctorSchedule")]

        public IActionResult ShowDoctorSchedule(int DoctorId)
        {
            if (TempData["DoctorId"] != null)
            {
                DoctorId = Convert.ToInt32( TempData["DoctorId"]);
            }
            var doctor = this._unitOfWork.DoctorRepository.RetriveItem(filter: e => e.Id == DoctorId, trancked: false);
            if (doctor != null)
            {
                var Schedules = this._unitOfWork.ScheduleRepository.Retrive(filter: e => e.DoctorId == DoctorId, includeProps: [e =>e.Doctor]);
                return View(Schedules.ToList());
            }
            return RedirectToAction(actionName: "NotFoundPage", controllerName: "Home", new {area = "Customer"});
        }
        [HttpGet]
        [Route("CreateDoctorSchedule")]
        public IActionResult CreateDoctorSchedule(CreateDoctorScheduleVM createDoctorScheduleVM)
        {
            return View(createDoctorScheduleVM);
        }

        [HttpPost]
        [Route("CreateDoctorSchedule")]
        public IActionResult CreateDoctorSchedule(int DoctorId)
        {
            var doctor = this._unitOfWork.DoctorRepository.RetriveItem(filter:e =>e.Id == DoctorId,  trancked:false ) as Doctor;
            if (doctor != null)
            {
                var numOfConstantant = (doctor.EndWork - doctor.StartWork).TotalMinutes/(doctor.ConsultationDuration);
                
                for(var day = DayOfWeek.Sunday; day <= DayOfWeek.Saturday; day++)
                {
                    var appointment = doctor.StartWork;
                    Schedule schedule = new Schedule
                    {
                        Appointment = appointment,
                        Available = true,
                        DoctorId = DoctorId,
                        Day = day
                    };

                    _unitOfWork.ScheduleRepository.Create(schedule);

                    for (int i = 1; i < numOfConstantant; i++)
                    {
                        appointment = appointment.AddMinutes(doctor.ConsultationDuration);

                        Schedule schedule1 = new Schedule
                        {
                            Appointment = appointment,
                            Available = true,
                            DoctorId = DoctorId,
                            Day = day
                        };
                        _unitOfWork.ScheduleRepository.Create(schedule1);
                    }
                }
                _unitOfWork.Commit();

            }


            return RedirectToAction("Index", "Doctor");
        }



    }
}
