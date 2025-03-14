using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub.IdentitySection;
using CMS.Models.CuraHub.PharmacySection;
using CMS.Models.CuraHub.PharmacySection.PharmacySectionVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Perestation.Layer.Areas.Customer.Controllers.CuraHub.Pharmacy
{
    [Area(nameof(Customer))]
    [Route("Customer/CuraHub/Pharmacy/Medicine")]
    public class MedicineController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private const int PageSize = 8;

        public MedicineController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        private void SetPaginationData(int pageNumber, int totalItems)
        {
            ViewBag.currentPage = pageNumber;
            ViewBag.lastPage = Math.Max((int)Math.Ceiling((double)totalItems / PageSize) - 1, 0);
        }

        [HttpGet]
        [Route("Index")]
        public IActionResult Index(int categoryId = 0, int pageNumber = 0)
        {
            var medicinesQuery = _unitOfWork.MedicineRepository.Retrive(
                categoryId == 0 ? null : e => e.PharmacyCategory.Id == categoryId,
                includeProps: [e => e.MedicineManufactory, e => e.PharmacyCategory]
            );

            int totalItems = medicinesQuery.Count();
            SetPaginationData(pageNumber, totalItems);

            var medicines = medicinesQuery.Skip(pageNumber * PageSize).Take(PageSize).ToList();
            ViewData["MedicineInCart"] = _unitOfWork.PharmacyCartRepository.Retrive().ToList();

            return View(medicines);
        }

        [HttpGet]
        [Route("Details")]
        public IActionResult Details(int medicineId)
        {
            var medicine = _unitOfWork.MedicineRepository.RetriveItem(
                e => e.Id == medicineId, 
                includeProps: [e => e.MedicineManufactory, e => e.PharmacyCategory]
            );

            if (medicine == null) return NotFound();

            var medicineInCart = _unitOfWork.PharmacyCartRepository.RetriveItem(e => e.MedicineId == medicine.Id);
            var userId = _userManager.GetUserId(User);

            var medicineDetailsVM = new MedicineDetailsVM
            {
                Medicine = medicine,
                PharmacyCart = medicineInCart,
                UserId = userId
            };

            return View(medicineDetailsVM);
        }

        [HttpGet]
        [Route("Search")]
        public IActionResult Search(string searchText, int pageNumber = 0)
        {
            var medicinesQuery = _unitOfWork.MedicineRepository.Retrive(e => e.Name.ToUpper().Contains(searchText.ToUpper()) || e.Description.ToUpper().Contains(searchText.ToUpper()));

            int totalItems = medicinesQuery.Count();
            SetPaginationData(pageNumber, totalItems);

            var medicines = medicinesQuery.Skip(pageNumber * PageSize).Take(PageSize).ToList();
            
            ViewData["MedicineInCart"] = _unitOfWork.PharmacyCartRepository.Retrive().ToList();

            return PartialView("_Search", medicines);
        }
    }
}

