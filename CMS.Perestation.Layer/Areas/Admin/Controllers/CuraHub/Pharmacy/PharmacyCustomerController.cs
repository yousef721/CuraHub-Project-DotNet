using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub.PharmacySection.PharmacySectionVM;
using CMS.Models.CuraHub.PharmacySection;

namespace CMS.Perestation.Layer.Areas.Admin.Controllers.CuraHub.Pharmacy
{
    [Area(nameof(Admin))]
    [Route("Admin/CuraHub/Pharmacy/pharmacyCustomer")]
    public class PharmacyCustomerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private const int PageSize = 8;

        public PharmacyCustomerController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        private void SetPaginationData(int pageNumber, int totalItems)
        {
            ViewBag.currentPage = pageNumber;
            ViewBag.lastPage = (int)Math.Ceiling((double)totalItems / PageSize) - 1;
        }

        [Route("Index")]
        public IActionResult Index(int pageNumber = 0)
        {
            var pharmacyCustomers = _unitOfWork.PharmacyCustomerRepository
                .Retrive()
                .Skip(pageNumber * PageSize)
                .Take(PageSize)
                .ToList();

            int totalItems = _unitOfWork.PharmacyCustomerRepository.Retrive().Count();
            SetPaginationData(pageNumber, totalItems);
            
            var pharmacyCustomerVM = _mapper.Map<List<PharmacyCustomerVM>>(pharmacyCustomers);
            return View(pharmacyCustomerVM);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create(int pageNumber = 0)
        {
            ViewBag.currentPage = pageNumber;
            return View();
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PharmacyCustomerVM pharmacyCustomerVM)
        {
            if (ModelState.IsValid)
            {   
                var pharmacyCustomer = _mapper.Map<PharmacyCustomer>(pharmacyCustomerVM);
                _unitOfWork.PharmacyCustomerRepository.Create(pharmacyCustomer);
                _unitOfWork.Commit();
                
                int totalItems = _unitOfWork.PharmacyCustomerRepository.Retrive().Count();
                SetPaginationData(0, totalItems);
                
                return RedirectToAction(nameof(Index), new { pageNumber = ViewBag.lastPage });
            }
            return View(pharmacyCustomerVM);
        }

        [HttpGet]
        [Route("Edit")]
        public IActionResult Edit(int id, int pageNumber = 0)
        {
            var pharmacyCustomer = _unitOfWork.PharmacyCustomerRepository.RetriveItem(p => p.Id == id);
            if (pharmacyCustomer == null) return NotFound();

            var pharmacyCustomerVM = _mapper.Map<PharmacyCustomerVM>(pharmacyCustomer);
            ViewBag.currentPage = pageNumber;
            
            return View(pharmacyCustomerVM);
        }

        [HttpPost]
        [Route("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PharmacyCustomerVM pharmacyCustomerVM, int pageNumber = 0)
        {
            if (ModelState.IsValid)
            {
                var pharmacyCustomer = _mapper.Map<PharmacyCustomer>(pharmacyCustomerVM);
                _unitOfWork.PharmacyCustomerRepository.Update(pharmacyCustomer);
                _unitOfWork.Commit();
                return RedirectToAction(nameof(Index), new { pageNumber });
            }
            ViewBag.currentPage = pageNumber;
            return View(pharmacyCustomerVM);
        }

        [HttpGet]
        [Route("Details")]
        public IActionResult Details(int id, int pageNumber = 0)
        {
            var pharmacyCustomer = _unitOfWork.PharmacyCustomerRepository.RetriveItem(d => d.Id == id, [e => e.PharmacyOrders]);
            if (pharmacyCustomer == null) return NotFound();

            var pharmacyCustomerVM = _mapper.Map<PharmacyCustomerVM>(pharmacyCustomer);
            ViewBag.currentPage = pageNumber;
            return View(pharmacyCustomerVM);
        }

        [HttpGet]
        [Route("Delete")]
        public IActionResult Delete(int id, int pageNumber = 0)
        {
            var pharmacyCustomer = _unitOfWork.PharmacyCustomerRepository.RetriveItem(p => p.Id == id);
            if (pharmacyCustomer == null) return NotFound();

            _unitOfWork.PharmacyCustomerRepository.Delete(pharmacyCustomer);
            _unitOfWork.Commit();
            return RedirectToAction("Index", new { pageNumber });
        }

        [HttpGet]
        [Route("Search")]
        public IActionResult Search(string searchText, int pageNumber = 0)
        {
            var customers = _unitOfWork.PharmacyCustomerRepository
                .Retrive(e => e.FirstName.ToLower().Contains(searchText.ToLower()) || e.LastName.ToLower().Contains(searchText.ToLower()) || e.Email.ToLower().Contains(searchText.ToLower()) || e.Phone.Contains(searchText))
                .Skip(pageNumber * PageSize)
                .Take(PageSize)
                .ToList();

            var customersVM = _mapper.Map<List<PharmacyCustomerVM>>(customers);
            return PartialView("_Search", customersVM);
        }
    }
}
