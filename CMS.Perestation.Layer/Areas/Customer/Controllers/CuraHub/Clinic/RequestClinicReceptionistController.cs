using AutoMapper;
using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.RequestDoctorSectionVM;
using CMS.Models.CuraHub.ClinicSection;
using CMS.Models.CuraHub.IdentitySection;
using CMS.Utitlities.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.RequestClinicReceptionistSectionVM;

namespace CMS.Perestation.Layer.Areas.Customer.Controllers.CuraHub.Clinic
{
    [Area(nameof(Customer))]
    [Route("Customer/CuraHub/Clinic/RequestClinicReceptionist")]
    public class RequestClinicReceptionistController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public RequestClinicReceptionistController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._userManager = userManager;

        }

        [HttpGet]
        [Route("CreateRequestClinicReceptionist")]
        public IActionResult CreateRequestClinicReceptionist()
        {
            var user = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
            Cust_RequestClinicReceptionistCreateVM cust_RequestClinicReceptionistCreateVM = new Cust_RequestClinicReceptionistCreateVM();

            if (user == null)
            {

                return RedirectToAction("Login", "Account", new { area = "Identity" });

            }
            cust_RequestClinicReceptionistCreateVM.ApplicationUserId = user.Id;
            cust_RequestClinicReceptionistCreateVM.Doctors = this._unitOfWork.DoctorRepository.Retrive().ToList();
            return View(cust_RequestClinicReceptionistCreateVM);

        }
        [HttpPost]
        [Route("CreateRequestClinicReceptionist")]
        public IActionResult CreateRequestClinicReceptionist(Cust_RequestClinicReceptionistCreateVM cust_RequestClinicReceptionistCreateVM)
        {
            #region Upload Files

            if (cust_RequestClinicReceptionistCreateVM.ProfilePictureFile != null && cust_RequestClinicReceptionistCreateVM.ProfilePictureFile.Length > 0)
            {
                cust_RequestClinicReceptionistCreateVM.ProfilePicture = FileOperation.UploadFile(cust_RequestClinicReceptionistCreateVM.ProfilePictureFile, "Images\\ClinicReceptionistPictures");
            }

            if (cust_RequestClinicReceptionistCreateVM.PersonalNationalIDCardFile != null && cust_RequestClinicReceptionistCreateVM.PersonalNationalIDCardFile.Length > 0)
            {
                cust_RequestClinicReceptionistCreateVM.PersonalNationalIDCard = FileOperation.UploadFile(cust_RequestClinicReceptionistCreateVM.PersonalNationalIDCardFile, "PersonalNationalIDCard");

            }

            
            #endregion

            #region ModelState Remove
            ModelState.Remove("ProfilePicture");
            ModelState.Remove("ProfilePictureFile");
            ModelState.Remove("PersonalNationalIDCard");
            ModelState.Remove("PersonalNationalIDCardFile");
            ModelState.Remove("Doctors");
            ModelState.Remove("ApplicationUsers");
            ModelState.Remove("Doctor");

            #endregion


            if (ModelState.IsValid)
            {


                var requestClinicReceptionist = _mapper.Map<RequestClinicReceptionist>(cust_RequestClinicReceptionistCreateVM);

                _unitOfWork.RequestClinicReceptionistRepository.Create(requestClinicReceptionist);
                _unitOfWork.Commit();

                return RedirectToAction("Index", controllerName: "Home", new { area = "Customer" });
            }
            return RedirectToAction("CreateRequestClinicReceptionist", routeValues: cust_RequestClinicReceptionistCreateVM);

        }
    }
}
