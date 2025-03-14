using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub.ClinicSection;
using CMS.Utitlities.StaticData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMS.Perestation.Layer.Areas.Admin.Controllers.CuraHub.Clinic
{
    [Area(nameof(Admin))]
    [Authorize(Roles = ($"{Role.AdminRole}"))]
    [Route("Admin/CuraHub/Clinic/PatientAppointment")]
    public class PatientAppointmentController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public PatientAppointmentController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }


        [Route("Index")]
        public IActionResult Index(string? query = null, int PageNumber = 1)
        {
            var patientAppointments = _unitOfWork.PatientAppointmentRepository.Retrive(includeProps: [e =>e.Patient]).Include(e => e.Schedule).ThenInclude(e=>e.Doctor);

            if (PageNumber < 1) PageNumber = 1;
            var appointments = patientAppointments.Skip((PageNumber - 1) * 5).Take(5);

            return View(appointments.ToList());
        }
    }
}
