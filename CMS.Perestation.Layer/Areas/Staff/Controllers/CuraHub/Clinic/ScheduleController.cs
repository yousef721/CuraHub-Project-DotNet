using AutoMapper;
using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Utitlities.StaticData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Perestation.Layer.Areas.Staff.Controllers.CuraHub.Clinic
{
    [Area(nameof(Staff))]
    [Authorize(Roles = ($"{Role.DoctorRole}"))]
    [Route("Staff/CuraHub/Clinic/Schedule")]
    public class ScheduleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ScheduleController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("LockAvailable")]
        public IActionResult LockAvailable(int ScheduleId, int DoctorId)
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
        public IActionResult UnLockAvailable(int ScheduleId, int DoctorId)
        {
            var schedule = this._unitOfWork.ScheduleRepository.RetriveItem(filter: e => e.Id == ScheduleId);
            if (schedule != null)
            {
                schedule.Available = true;
                this._unitOfWork.ScheduleRepository.Update(schedule);
                this._unitOfWork.Commit();
            }
            TempData["DoctorId"] = DoctorId;
            return RedirectToAction("ShowDoctorSchedule", routeValues: DoctorId);
        }

        [Route("ShowDoctorSchedule")]

        public IActionResult ShowDoctorSchedule(int DoctorId)
        {
            if (TempData["DoctorId"] != null)
            {
                DoctorId = Convert.ToInt32(TempData["DoctorId"]);
            }
            var doctor = this._unitOfWork.DoctorRepository.RetriveItem(filter: e => e.Id == DoctorId, trancked: false);
            if (doctor != null)
            {
                var Schedules = this._unitOfWork.ScheduleRepository.Retrive(filter: e => e.DoctorId == DoctorId, includeProps: [e => e.Doctor]);
                return View(Schedules.ToList());
            }
            return RedirectToAction(actionName: "NotFoundPage", controllerName: "Home", new { area = "Customer" });
        }
    }
}
