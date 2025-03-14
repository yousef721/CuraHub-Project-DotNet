using AutoMapper;
using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub.ClinicSection;
using CMS.Models.CuraHub.ClinicSection.ClinicSectionVM;
using CMS.Models.CuraHub.IdentitySection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace CMS.Perestation.Layer.Areas.Customer.Controllers.CuraHub.Clinic
{
    [Area(nameof(Customer))]
    [Route("Customer/CuraHub/Clinic/DoctorReview")]
    public class DoctorReviewController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;


        public DoctorReviewController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._userManager = userManager;
        }
        [Route("Index")]
        public IActionResult Index(int DoctorId)
        {
            var reviews = this._unitOfWork.DoctorReviewRepository.Retrive(filter: e => e.DoctorId == DoctorId,
                includeProps: [e => e.Doctor, e => e.ApplicationUser]).ToList();
            return View(reviews);
        }
        [HttpPost]
        [Route("Create")]
        public IActionResult Create(DoctorReview doctorReview) 
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var user = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
                doctorReview.ApplicationUserId = user.Id??"";
                doctorReview.Date = DateOnly.FromDateTime(DateTime.Now);
                _unitOfWork.DoctorReviewRepository.Create(doctorReview);
                _unitOfWork.Commit();
                return RedirectToAction(nameof(Details), "Doctor",routeValues: new { doctorReview.DoctorId });
            }
            return RedirectToAction("Login", "Account", new { area = "Identity" });

        }
    }
}
