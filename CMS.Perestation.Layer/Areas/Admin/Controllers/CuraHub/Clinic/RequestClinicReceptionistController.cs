using AutoMapper;
using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.ClinicReceptionistVM;
using CMS.Models.CuraHub.ClinicSection;
using CMS.Models.CuraHub.IdentitySection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.RequestClinicReceptionistSectionVM;
using CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.ScheduleVM;
using CMS.Utitlities.Helper;
using CMS.Utitlities.StaticData;
using Microsoft.AspNetCore.Authorization;

namespace CMS.Perestation.Layer.Areas.Admin.Controllers.CuraHub.Clinic
{
    [Area(nameof(Admin))]
    [Authorize(Roles = ($"{Role.AdminRole}"))]
    [Route("Admin/CuraHub/Clinic/RequestClinicReceptionist")]
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

        [Route("Index")]
        public IActionResult Index(string? query = null, int PageNumber = 1, int? DoctorId = null)
        {
            var RequestClinicReceptionists = _unitOfWork.RequestClinicReceptionistRepository.Retrive(includeProps: [e => e.Doctor]);
            if (query != null)
            {
                query = query.Trim();

                RequestClinicReceptionists = RequestClinicReceptionists.Where(e => e.FirstName.Contains(query) || e.LastName.Contains(query) || e.PersonalNationalIDNumber.Contains(query));
            }
            if (DoctorId != null)
            {

                RequestClinicReceptionists = RequestClinicReceptionists.Where(e => e.DoctorId == DoctorId);
            }
            if (PageNumber < 1) PageNumber = 1;
            Admin_RequestClinicReceptionistsVM admin_RequestClinicReceptionistsVM = new Admin_RequestClinicReceptionistsVM();
            admin_RequestClinicReceptionistsVM.TotalRequestClinicReceptionistCount = RequestClinicReceptionists.Count();
            admin_RequestClinicReceptionistsVM.CurrentPageNumber = PageNumber;
            RequestClinicReceptionists = RequestClinicReceptionists.Skip((PageNumber - 1) * 5).Take(5);
            admin_RequestClinicReceptionistsVM.RequestClinicReceptionists = RequestClinicReceptionists.ToList();
            admin_RequestClinicReceptionistsVM.Doctors = this._unitOfWork.DoctorRepository.Retrive().ToList();


            return View(admin_RequestClinicReceptionistsVM);
        }
        [Route(nameof(Details))]
        public IActionResult Details(int RequestClinicReceptionistId)
        {
            var RequestClinicReceptionist = this._unitOfWork.RequestClinicReceptionistRepository.RetriveItem(filter: e => e.Id == RequestClinicReceptionistId, includeProps: [e => e.Doctor]) as RequestClinicReceptionist;
            if (RequestClinicReceptionist != null)
            {
                var requestClinicReceptionistDetailsVM = this._mapper.Map<RequestClinicReceptionistDetailsVM>(RequestClinicReceptionist);
                return View(requestClinicReceptionistDetailsVM);
            }
            return RedirectToAction(nameof(Index));
        }

        [Route("Accept")]
        public IActionResult Accept(int RequestClinicReceptionistId)
        {
            var request = _unitOfWork.RequestClinicReceptionistRepository.RetriveItem(filter: e => e.Id == RequestClinicReceptionistId) as RequestClinicReceptionist;
            if (request != null)
            {
                var clinicReceptionist = _mapper.Map<ClinicReceptionist>(request);
                clinicReceptionist.Id = 0;
                _unitOfWork.ClinicReceptionistRepository.Create(clinicReceptionist);
                _unitOfWork.RequestClinicReceptionistRepository.Delete(request);
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

            }
            return RedirectToAction(nameof(Index));

        }
        [Route("Reject")]
        public IActionResult Reject(int RequestClinicReceptionistId)
        {
            var request = _unitOfWork.RequestClinicReceptionistRepository.RetriveItem(filter: e => e.Id == RequestClinicReceptionistId) as RequestClinicReceptionist;
            if (request != null)
            {
                if (request.ProfilePicture != "clinicRecepiatist.jpg")
                {
                    FileOperation.DeleteFile(request.ProfilePicture, "Images\\ClinicReceptionistPictures");

                }

                _unitOfWork.RequestClinicReceptionistRepository.Delete(request);
                _unitOfWork.Commit();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
