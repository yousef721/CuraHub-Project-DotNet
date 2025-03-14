using AutoMapper;
using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.ClinicReceptionistVM;
using CMS.Models.CuraHub.ClinicSection;
using CMS.Models.CuraHub.IdentitySection;
using CMS.Utitlities.StaticData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Perestation.Layer.Areas.Staff.Controllers.CuraHub.Clinic
{
    [Area(nameof(Staff))]
    [Authorize(Roles = $"{Role.DoctorRole},{Role.ClinicReceptionistRole}")]
    [Route("Staff/CuraHub/Clinic/ClinicReceptionist")]
    public class ClinicReceptionistController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public ClinicReceptionistController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._userManager = userManager;

        }
        [Route("Index")]
        public IActionResult Index(int DoctorId ,string ? query = null, int PageNumber = 1)
        {
            var clinicReceptionists = _unitOfWork.ClinicReceptionistRepository.Retrive(filter:e =>e.DoctorId == DoctorId , includeProps: [e => e.Doctor]);
            if (query != null)
            {
                query = query.Trim();

                clinicReceptionists = clinicReceptionists.Where(e => e.FirstName.Contains(query) || e.LastName.Contains(query) || e.PersonalNationalIDNumber.Contains(query));
            }
           
            if (PageNumber < 1) PageNumber = 1;
            ClinicReceptionistsVM clinicReceptionistsVM = new ClinicReceptionistsVM();
            clinicReceptionistsVM.TotalClinicReceptionistCount = clinicReceptionists.Count();
            clinicReceptionistsVM.CurrentPage = PageNumber;
            clinicReceptionists = clinicReceptionists.Skip((PageNumber - 1) * 5).Take(5);
            clinicReceptionistsVM.clinicReceptionists = clinicReceptionists.ToList();


            return View(clinicReceptionistsVM);
        }
        [Route(nameof(Details))]
        public IActionResult Details(int ClinicReceptionistId)
        {
            var clinicReceptionist = this._unitOfWork.ClinicReceptionistRepository.RetriveItem(filter: e => e.Id == ClinicReceptionistId, includeProps: [e => e.Doctor]) as ClinicReceptionist;
            if (clinicReceptionist != null)
            {
                var clinicReceptionistDetailsVM = this._mapper.Map<ClinicReceptionistDetailsVM>(clinicReceptionist);
                return View(clinicReceptionistDetailsVM);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
