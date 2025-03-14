using AutoMapper;
using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub.PharmacySection;
using CMS.Models.CuraHub.PharmacySection.PharmacySectionVM;
using Microsoft.AspNetCore.Mvc;


namespace CMS.Perestation.Layer.Areas.Admin.Controllers.CuraHub.Pharmacy
{
    [Area(nameof(Admin))]
    [Route("Admin/CuraHub/Pharmacy/Medicine")]
    public class MedicineController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private const int PageSize = 8;

        public MedicineController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        private CategoriesAndManufactoriesVM PopulateViewModel(MedicineVM? medicineVM = null)
        {
            var pharmacyCategories = _unitOfWork.PharmacyCategoryRepository.Retrive().ToList();
            var manufactories = _unitOfWork.MedicineManufactoryRepository.Retrive().ToList();
            CategoriesAndManufactoriesVM model = new CategoriesAndManufactoriesVM(){
                PharmacyCategories = pharmacyCategories,
                MedicineManufactories = manufactories,
                MedicinesVM = medicineVM
            };
            return model;
        }

        private string HandleImageUpload(IFormFile file, string oldImagePath = "")
        {
            if (file == null || file.Length == 0) return oldImagePath;

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Images", "Medicines", fileName);

            using (var stream = System.IO.File.Create(filePath))
            {
                file.CopyTo(stream);
            }

            if (!string.IsNullOrEmpty(oldImagePath))
            {
                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Images", "Medicines", oldImagePath);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            return fileName;
        }

        private void SetPaginationData(int pageNumber, int totalItems)
        {
            ViewBag.currentPage = pageNumber;
            ViewBag.lastPage = (int)Math.Ceiling((double)totalItems / PageSize) - 1;
        }
        [HttpGet]
        [Route("Index")]
        public ActionResult Index(int pageNumber = 0)
        {
            var medicines = _unitOfWork.MedicineRepository
            .Retrive(includeProps: [e => e.MedicineManufactory, e => e.PharmacyCategory])
            .Skip(pageNumber * PageSize)
            .Take(PageSize)
            .ToList();
                
            int totalItems = _unitOfWork.MedicineRepository.Retrive().Count();
            SetPaginationData(pageNumber, totalItems);

            var medicineVM = _mapper.Map<List<MedicineVM>>(medicines);
            return View(medicineVM);
        }
        [HttpGet]
        [Route("Create")]
        public ActionResult Create(int pageNumber = 0)
        {
            ViewBag.currentPage = pageNumber;
            return View(PopulateViewModel());
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoriesAndManufactoriesVM modelVM)
        {
            ModelState.Remove("MedicinesVM.Img");
            if (ModelState.IsValid)
            {
                modelVM.MedicinesVM.Img = HandleImageUpload(modelVM.MedicinesVM.File);

                var medicine = _mapper.Map<Medicine>(modelVM.MedicinesVM);
                _unitOfWork.MedicineRepository.Create(medicine);
                _unitOfWork.Commit();

                int totalItems = _unitOfWork.MedicineRepository.Retrive().Count();
                SetPaginationData(0, totalItems);
                
                return RedirectToAction(nameof(Index), new { pageNumber = ViewBag.lastPage });
            }

            return View(PopulateViewModel(modelVM.MedicinesVM));
        }

        [HttpGet]
        [Route("Edit")]
        public ActionResult Edit(int id, int pageNumber)
        {
            var medicine = _unitOfWork.MedicineRepository.RetriveItem(e => e.Id == id);
            if (medicine == null) return NotFound();

            var medicineVM = _mapper.Map<MedicineVM>(medicine);
            ViewBag.currentPage = pageNumber;

            return View(PopulateViewModel(medicineVM));
        }

        [HttpPost]
        [Route("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoriesAndManufactoriesVM modelVM, int pageNumber = 0)
        {
            ModelState.Remove("MedicinesVM.Img");
            ModelState.Remove("MedicinesVM.File");

            if (ModelState.IsValid)
            {
                modelVM.MedicinesVM.Img = HandleImageUpload(modelVM.MedicinesVM.File, modelVM.MedicinesVM.Img);

                var medicine = _mapper.Map<Medicine>(modelVM.MedicinesVM);
                _unitOfWork.MedicineRepository.Update(medicine);
                _unitOfWork.Commit();

                return RedirectToAction(nameof(Index), new { pageNumber });
            }

            ViewBag.currentPage = pageNumber;
            return View(PopulateViewModel(modelVM.MedicinesVM));
        }

        [Route(nameof(Details))]
        public ActionResult Details(int id, int pageNumber = 0)
        {
            var medicine = _unitOfWork.MedicineRepository.RetriveItem(e => e.Id == id, includeProps: [e => e.MedicineManufactory, e => e.PharmacyCategory]);
            if (medicine == null) return NotFound();

            ViewBag.currentPage = pageNumber;
            var medicineVM = _mapper.Map<MedicineVM>(medicine);
            return View(medicineVM);
        }

        [HttpGet]
        [Route("Delete")]
        public ActionResult Delete(int id, int pageNumber = 0)
        {
            var medicine = _unitOfWork.MedicineRepository.RetriveItem(e => e.Id == id);
            if (medicine == null) return NotFound();

            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Images", "Medicines", medicine.Img);

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            _unitOfWork.MedicineRepository.Delete(medicine);
            _unitOfWork.Commit();

            // TempData["Success"] = "Medicine deleted successfully.";
            return RedirectToAction("Index", new { pageNumber });
        }

        [HttpGet]
        [Route("Search")]
        public IActionResult Search(string searchText, int pageNumber = 0)
        {
            var medicines = _unitOfWork.MedicineRepository
                .Retrive(e => e.Name.ToLower().Contains(searchText.ToLower()), includeProps: [e => e.MedicineManufactory, e => e.PharmacyCategory])
                .Skip(pageNumber * PageSize)
                .Take(PageSize)
                .ToList();

            var medicineVM = _mapper.Map<List<MedicineVM>>(medicines);
            return PartialView("_Search", medicineVM);
        }
    }
}
