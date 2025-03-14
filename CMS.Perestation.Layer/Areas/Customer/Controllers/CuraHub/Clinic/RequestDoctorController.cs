using AutoMapper;
using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub.ClinicSection;
using CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.RequestDoctorSectionVM;
using CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.ScheduleVM;
using CMS.Models.CuraHub.IdentitySection;
using CMS.Utitlities.Helper;
using CMS.Utitlities.StaticData;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Perestation.Layer.Areas.Customer.Controllers.CuraHub.Clinic
{
    [Area(nameof(Customer))]
    [Route("Customer/CuraHub/Clinic/RequestDoctor")]
    public class RequestDoctorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;


        public RequestDoctorController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._userManager = userManager;
        }


        [HttpGet]
        [Route("CreateRequestDoctor")]
        public IActionResult CreateRequestDoctor()
        {
            var user = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
            Cust_RequestDoctorCreateVM cust_RequestDoctorCreateVM = new Cust_RequestDoctorCreateVM();
            
            if (user == null)
            {

                return RedirectToAction("Login", "Account", new { area = "Identity" });

            }
            cust_RequestDoctorCreateVM.ApplicationUserId = user.Id;
            cust_RequestDoctorCreateVM.Specializations = this._unitOfWork.SpecializationRepository.Retrive().ToList();
            return View(cust_RequestDoctorCreateVM);

        }
        [HttpPost]
        [Route("CreateRequestDoctor")]
        public IActionResult CreateRequestDoctor(Cust_RequestDoctorCreateVM cust_RequestDoctorCreateVM)
        {
            #region Upload Files

            if (cust_RequestDoctorCreateVM.ProfilePictureFile != null && cust_RequestDoctorCreateVM.ProfilePictureFile.Length > 0)
            {
                cust_RequestDoctorCreateVM.ProfilePicture = FileOperation.UploadFile(cust_RequestDoctorCreateVM.ProfilePictureFile, "Images\\DoctorsPictures");
            }

            if (cust_RequestDoctorCreateVM.PersonalNationalIDCardFile != null && cust_RequestDoctorCreateVM.PersonalNationalIDCardFile.Length > 0)
            {
                cust_RequestDoctorCreateVM.PersonalNationalIDCard = FileOperation.UploadFile(cust_RequestDoctorCreateVM.PersonalNationalIDCardFile, "PersonalNationalIDCard");

            }

            if (cust_RequestDoctorCreateVM.MedicalDegreeFile != null && cust_RequestDoctorCreateVM.MedicalDegreeFile.Length > 0)
            {
                cust_RequestDoctorCreateVM.MedicalDegree = FileOperation.UploadFile(cust_RequestDoctorCreateVM.MedicalDegreeFile, "MedicalDegree");

            }

            if (cust_RequestDoctorCreateVM.MedicalLicenseFile != null && cust_RequestDoctorCreateVM.MedicalLicenseFile.Length > 0)
            {
                cust_RequestDoctorCreateVM.MedicalLicense = FileOperation.UploadFile(cust_RequestDoctorCreateVM.MedicalLicenseFile, "MedicalLicense");

            }

            if (cust_RequestDoctorCreateVM.MedicalRegistrationFile != null && cust_RequestDoctorCreateVM.MedicalRegistrationFile.Length > 0)
            {
                cust_RequestDoctorCreateVM.MedicalRegistration = FileOperation.UploadFile(cust_RequestDoctorCreateVM.MedicalRegistrationFile, "MedicalRegistration");

            }

            if (cust_RequestDoctorCreateVM.MedicalIdentificationCardFile != null && cust_RequestDoctorCreateVM.MedicalIdentificationCardFile.Length > 0)
            {
                cust_RequestDoctorCreateVM.MedicalIdentificationCard = FileOperation.UploadFile(cust_RequestDoctorCreateVM.MedicalIdentificationCardFile, "MedicalIdentificationCard");

            }
            #endregion

            #region ModelState Remove
            ModelState.Remove("ProfilePicture");
            ModelState.Remove("ProfilePictureFile");
            ModelState.Remove("PersonalNationalIDCard");
            ModelState.Remove("MedicalDegree");
            ModelState.Remove("MedicalLicense");
            ModelState.Remove("MedicalRegistration");
            ModelState.Remove("MedicalIdentificationCard");
            ModelState.Remove("PersonalNationalIDCardFile");
            ModelState.Remove("MedicalDegreeFile");
            ModelState.Remove("MedicalLicenseFile");
            ModelState.Remove("MedicalRegistrationFile");
            ModelState.Remove("MedicalIdentificationCardFile");
            ModelState.Remove("CrudDoctorOption");
            ModelState.Remove("Specializations");
            ModelState.Remove("ApplicationUsers");
            ModelState.Remove("Specialization");

            #endregion


            if (ModelState.IsValid)
            {


                var requestDoctor = _mapper.Map<RequestDoctor>(cust_RequestDoctorCreateVM);

                _unitOfWork.RequestDoctorRepository.Create(requestDoctor);
                _unitOfWork.Commit();

                return RedirectToAction("Index", controllerName: "Home", new {area="Customer"});
            }
            return RedirectToAction("CreateRequestDoctor", routeValues: cust_RequestDoctorCreateVM);

        }
    }
}
