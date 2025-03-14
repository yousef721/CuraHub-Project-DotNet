using AutoMapper;
using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub.ClinicSection;
using CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.History;
using CMS.Models.CuraHub.IdentitySection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Perestation.Layer.Areas.Customer.Controllers.CuraHub.Clinic
{
    [Area(nameof(Customer))]
    [Route("Customer/CuraHub/Clinic/PatientHistory")]
    public class PatientHistoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;


        public PatientHistoryController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        [Route("Index")]
        public IActionResult Index(int PatientId)
        {
            var Histories = this._unitOfWork.PatientHistoryRepository.Retrive(filter: e=>e.PatientId == PatientId);
            PatientHistoriesVM patientHistoriesVM = new PatientHistoriesVM();
            patientHistoriesVM.PatientId = PatientId;
            patientHistoriesVM.Histories = Histories.ToList();
            return View(patientHistoriesVM);
        }
        [HttpGet]
        [Route("Create")]
        public IActionResult Create(int PatientId)
        {
            var patient = this._unitOfWork.PatientRepository.RetriveItem(filter: e=>e.Id == PatientId);
            if(patient != null)
            {
                return View(patient);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [Route("Create")]
        public IActionResult Create(PatientHistoryCreateVM patientHistoryCreateVM)
        {
            ModelState.Remove("Patient");
            if (ModelState.IsValid)
            {
                var history = this._mapper.Map<PatientHistory>(patientHistoryCreateVM);
                this._unitOfWork.PatientHistoryRepository.Create(history);
                this._unitOfWork.Commit();
            }
            //var returnUrl = TempData["returnUrl"] as string;
            //if (!string.IsNullOrEmpty(returnUrl))
            //{
            //    return Redirect(returnUrl);
            //}
            return RedirectToAction("Index" , routeValues: new {patientHistoryCreateVM.PatientId});

        }
    }
}
