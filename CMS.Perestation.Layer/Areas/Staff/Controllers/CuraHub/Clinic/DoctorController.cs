using AutoMapper;
using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub.IdentitySection;
using CMS.Utitlities.StaticData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Perestation.Layer.Areas.Staff.Controllers.CuraHub.Clinic
{
    [Area(nameof(Staff))]
    [Authorize(Roles = ($"{Role.DoctorRole}"))]
    [Route("Staff/CuraHub/Clinic/Doctor")]
    public class DoctorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;


        public DoctorController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        [Route("Info")]
        public IActionResult Info(int DoctorId)
        {
            var doctor = this._unitOfWork.DoctorRepository.RetriveItem(filter: e => e.Id == DoctorId , includeProps: [e =>e.Specialization] );
            if(doctor != null) 
            {
                return View(doctor);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
