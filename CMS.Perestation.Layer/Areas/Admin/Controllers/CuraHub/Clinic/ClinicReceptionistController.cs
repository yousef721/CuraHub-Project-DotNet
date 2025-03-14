using AutoMapper;
using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub.ClinicSection;
using CMS.Models.CuraHub.ClinicSection.ClinicSectionVM;
using CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.ClinicReceptionistVM;
using CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.ScheduleVM;
using CMS.Models.CuraHub.IdentitySection;
using CMS.Utitlities.Helper;
using CMS.Utitlities.StaticData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Numerics;

namespace CMS.Perestation.Layer.Areas.Admin.Controllers.CuraHub.Clinic
{
    [Area(nameof(Admin))]
    [Authorize(Roles = ($"{Role.AdminRole}"))]
    [Route("Admin/CuraHub/Clinic/ClinicReceptionist")]
    public class ClinicReceptionistController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public ClinicReceptionistController(IUnitOfWork unitOfWork, IMapper mapper , UserManager<ApplicationUser> userManager)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._userManager = userManager;

        }
        [Route("Index")]
        public IActionResult Index(string? query = null, int PageNumber = 1, int? DoctorId = null)
        {
            var clinicReceptionists = _unitOfWork.ClinicReceptionistRepository.Retrive(includeProps: [e => e.Doctor]);
            if (query != null)
            {
                query = query.Trim();

                clinicReceptionists = clinicReceptionists.Where(e => e.FirstName.Contains(query) || e.LastName.Contains(query) || e.PersonalNationalIDNumber.Contains(query));
            }
            if (DoctorId != null)
            {

                clinicReceptionists = clinicReceptionists.Where(e => e.DoctorId == DoctorId);
            }
            if (PageNumber < 1) PageNumber = 1;
            ClinicReceptionistsVM clinicReceptionistsVM = new ClinicReceptionistsVM();
            clinicReceptionistsVM.TotalClinicReceptionistCount = clinicReceptionists.Count();
            clinicReceptionistsVM.CurrentPage = PageNumber;
            clinicReceptionists = clinicReceptionists.Skip((PageNumber - 1) * 5).Take(5);
            clinicReceptionistsVM.clinicReceptionists = clinicReceptionists.ToList();
            clinicReceptionistsVM.doctors = this._unitOfWork.DoctorRepository.Retrive().ToList();


            return View(clinicReceptionistsVM);
        }
        [Route(nameof(Details))]
        public IActionResult Details(int ClinicReceptionistId)
        {
            var clinicReceptionist = this._unitOfWork.ClinicReceptionistRepository.RetriveItem(filter: e => e.Id == ClinicReceptionistId, includeProps: [e => e.Doctor]) as ClinicReceptionist;
            if(clinicReceptionist != null)
            {
                var clinicReceptionistDetailsVM = this._mapper.Map<ClinicReceptionistDetailsVM>(clinicReceptionist);
                return View(clinicReceptionistDetailsVM);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("CreateEdit")]
        public IActionResult CreateEdit(int ClinicReceptionistId = 0)
        {
            var clinicReceptionist = _unitOfWork.ClinicReceptionistRepository.RetriveItem(filter: e => e.Id == ClinicReceptionistId);
            ClinicReceptionistCreateEditVM doctorCreateEditVM = new ClinicReceptionistCreateEditVM();
            if (clinicReceptionist != null)
            {
                doctorCreateEditVM = _mapper.Map<ClinicReceptionistCreateEditVM>(clinicReceptionist);
                doctorCreateEditVM.CrudClinicReceptionistOption = CMS.Models.Enums.CrudOption.Editing;
            }
            doctorCreateEditVM.Doctors = this._unitOfWork.DoctorRepository.Retrive().ToList();
            doctorCreateEditVM.ApplicationUsers = this._unitOfWork.ApplicationUserRepository.Retrive().ToList();
            return View(doctorCreateEditVM);
        }
        [HttpPost]
        [Route("CreateEdit")]
        public IActionResult CreateEdit(ClinicReceptionistCreateEditVM clinicReceptionistCreateEditVM)
        {
            if (clinicReceptionistCreateEditVM.ProfilePictureFile != null && clinicReceptionistCreateEditVM.ProfilePictureFile.Length > 0)
            {
                clinicReceptionistCreateEditVM.ProfilePicture = FileOperation.UploadFile(clinicReceptionistCreateEditVM.ProfilePictureFile, "Images\\ClinicReceptionistPictures");
            }

            if (clinicReceptionistCreateEditVM.PersonalNationalIDCardFile != null && clinicReceptionistCreateEditVM.PersonalNationalIDCardFile.Length > 0)
            {
                clinicReceptionistCreateEditVM.PersonalNationalIDCard = FileOperation.UploadFile(clinicReceptionistCreateEditVM.PersonalNationalIDCardFile, "PersonalNationalIDCard");

            }
            if (clinicReceptionistCreateEditVM.CrudClinicReceptionistOption == CMS.Models.Enums.CrudOption.Creating)
            {
                return RedirectToAction(actionName: nameof(Create), controllerName: nameof(ClinicReceptionist), routeValues: clinicReceptionistCreateEditVM);

            }
            return RedirectToAction(actionName: nameof(Edit), controllerName: nameof(ClinicReceptionist), routeValues: clinicReceptionistCreateEditVM);
        }
        [Route("Create")]
        public IActionResult Create(ClinicReceptionistCreateEditVM clinicReceptionistCreateEditVM)
        {
            ModelState.Remove("ProfilePicture");
            ModelState.Remove("ProfilePictureFile");
            ModelState.Remove("PersonalNationalIDCard");
            ModelState.Remove("PersonalNationalIDCardFile");
            ModelState.Remove("ApplicationUsers");
            ModelState.Remove("Doctor");
            ModelState.Remove("Doctors");

            if (ModelState.IsValid)
            {


                var clinicReceptionist = _mapper.Map<ClinicReceptionist>(clinicReceptionistCreateEditVM);

                _unitOfWork.ClinicReceptionistRepository.Create(clinicReceptionist);
                _unitOfWork.Commit();

                var savedClinicReceptionist = _unitOfWork.ClinicReceptionistRepository.RetriveItem(filter: e => e.PersonalNationalIDNumber == clinicReceptionist.PersonalNationalIDNumber);

                if (savedClinicReceptionist != null)
                {
                    // Change Role to Doctor
                    var user = _userManager.FindByIdAsync(savedClinicReceptionist.ApplicationUserId).GetAwaiter().GetResult();
                    if (user != null)
                    {
                        _userManager.RemoveFromRolesAsync(user, _userManager.GetRolesAsync(user).GetAwaiter().GetResult()).GetAwaiter().GetResult();
                        _userManager.AddToRoleAsync(user, Role.ClinicReceptionistRole).GetAwaiter().GetResult();
                    }
                }



                return RedirectToAction(nameof(Index));

            }
            return RedirectToAction("CreateEdit", routeValues: clinicReceptionistCreateEditVM);

        }
        [Route("Edit")]
        public IActionResult Edit(ClinicReceptionistCreateEditVM clinicReceptionistCreateEditVM)
        {
            ModelState.Remove("ProfilePicture");
            ModelState.Remove("ProfilePictureFile");
            ModelState.Remove("PersonalNationalIDCard");
            ModelState.Remove("PersonalNationalIDCardFile");
            ModelState.Remove("ApplicationUsers");
            ModelState.Remove("Doctor");
            ModelState.Remove("Doctors");

            if (ModelState.IsValid)
            {
                var oldClinicReceptionist = _unitOfWork.ClinicReceptionistRepository.RetriveItem(filter: e => e.Id == clinicReceptionistCreateEditVM.Id, trancked: false);
                if(oldClinicReceptionist != null)
                {
                    if (clinicReceptionistCreateEditVM.ProfilePicture == "clinicRecepiatist.jpg")
                    {
                        clinicReceptionistCreateEditVM.ProfilePicture = oldClinicReceptionist.ProfilePicture;
                    }
                    else
                    {
                        if (oldClinicReceptionist.ProfilePicture != "clinicRecepiatist.jpg")
                        {
                            FileOperation.DeleteFile(oldClinicReceptionist.ProfilePicture, "Images\\ClinicReceptionistPictures");

                        }
                    }

                    if (clinicReceptionistCreateEditVM.PersonalNationalIDCard == "PersonalNationalIDCardDefault.jpg")
                    {

                        clinicReceptionistCreateEditVM.PersonalNationalIDCard = oldClinicReceptionist.PersonalNationalIDCard;
                    }
                    else
                    {
                        if (oldClinicReceptionist.PersonalNationalIDCard != "PersonalNationalIDCardDefault.jpg")
                        {
                            FileOperation.DeleteFile(oldClinicReceptionist.PersonalNationalIDCard, "PersonalNationalIDCard");
                        }

                    }

                    var clinicReceptionist = this._mapper.Map<ClinicReceptionist>(clinicReceptionistCreateEditVM);
                    _unitOfWork.ClinicReceptionistRepository.Update(clinicReceptionist);
                    _unitOfWork.Commit();
                }
                return RedirectToAction(nameof(Index));

            }
            return NotFound();

        }
        [Route("Delete")]
        public IActionResult Delete(int ClinicReceptionistId)
        {
            var clinicReceptionist = _unitOfWork.ClinicReceptionistRepository.RetriveItem(filter: e => e.Id == ClinicReceptionistId, trancked: false);

            if (clinicReceptionist != null)
            {
                if (clinicReceptionist.ProfilePicture != "clinicRecepiatist.jpg")
                {
                    FileOperation.DeleteFile(clinicReceptionist.ProfilePicture, "Images\\ClinicReceptionistPictures");
                }

                if (clinicReceptionist.PersonalNationalIDCard != "PersonalNationalIDCardDedault.jpg")
                {
                    FileOperation.DeleteFile(clinicReceptionist.PersonalNationalIDCard, "PersonalNationalIDCard");

                }

                this._unitOfWork.ClinicReceptionistRepository.Delete(clinicReceptionist);
                this._unitOfWork.Commit();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}