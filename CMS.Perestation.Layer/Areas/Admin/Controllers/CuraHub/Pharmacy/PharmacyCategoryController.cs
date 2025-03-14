using AutoMapper;
using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub.PharmacySection;
using CMS.Models.CuraHub.PharmacySection.PharmacySectionVM;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace CMS.Perestation.Layer.Areas.Admin.Controllers.CuraHub.Pharmacy
{
    [Area(nameof(Admin))]
    [Route("Admin/CuraHub/Pharmacy/PharmacyCategory")]
    public class PharmacyCategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private const int PageSize = 8;

        public PharmacyCategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        private string HandleImageUpload(IFormFile file, string oldImagePath = "")
        {
            if (file == null || file.Length == 0) return oldImagePath;

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Images", "PharmacyCategories", fileName);

            using (var stream = System.IO.File.Create(filePath))
            {
                file.CopyTo(stream);
            }

            if (!string.IsNullOrEmpty(oldImagePath))
            {
                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Images", "PharmacyCategories", oldImagePath);
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
        public IActionResult Index(int pageNumber = 0)
        {
            var categories = _unitOfWork.PharmacyCategoryRepository
                .Retrive()
                .Skip(pageNumber * PageSize)
                .Take(PageSize)
                .ToList();

            int totalItems = _unitOfWork.PharmacyCategoryRepository.Retrive().Count();
            SetPaginationData(pageNumber, totalItems);

            var categoriesVM = _mapper.Map<List<PharmacyCategoryVM>>(categories);
            return View(categoriesVM);
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
        public IActionResult Create(PharmacyCategoryVM pharmacyCategoryVM)
        {
            ModelState.Remove("Img");
            ModelState.Remove("File");
            
            if (ModelState.IsValid)
            {
                pharmacyCategoryVM.Img = HandleImageUpload(pharmacyCategoryVM.File);

                var category = _mapper.Map<PharmacyCategory>(pharmacyCategoryVM);
                _unitOfWork.PharmacyCategoryRepository.Create(category);
                _unitOfWork.Commit();

                int totalItems = _unitOfWork.PharmacyCategoryRepository.Retrive().Count();
                SetPaginationData(0, totalItems);
                
                return RedirectToAction(nameof(Index), new { pageNumber = ViewBag.lastPage });
            }

            return View(pharmacyCategoryVM);
        }

        [HttpGet]
        [Route("Edit")]
        public IActionResult Edit(int id, int pageNumber = 0)
        {
            var category = _unitOfWork.PharmacyCategoryRepository.RetriveItem(e => e.Id == id);
            if (category == null) return NotFound();

            var pharmacyCategoryVM = _mapper.Map<PharmacyCategoryVM>(category);
            ViewBag.currentPage = pageNumber;

            return View(pharmacyCategoryVM);
        }

        [HttpPost]
        [Route("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PharmacyCategoryVM pharmacyCategoryVM, int pageNumber = 0)
        {
            ModelState.Remove("Img");
            ModelState.Remove("File");

            if (ModelState.IsValid)
            {
                pharmacyCategoryVM.Img = HandleImageUpload(pharmacyCategoryVM.File, pharmacyCategoryVM.Img);

                var category = _mapper.Map<PharmacyCategory>(pharmacyCategoryVM);
                _unitOfWork.PharmacyCategoryRepository.Update(category);
                _unitOfWork.Commit();

                return RedirectToAction("Index", new { pageNumber });
            }

            ViewBag.currentPage = pageNumber;
            return View(pharmacyCategoryVM);
        }

        [HttpGet]
        [Route("Details")]
        public IActionResult Details(int id, int pageNumber = 0)
        {
            var category = _unitOfWork.PharmacyCategoryRepository.RetriveItem(e => e.Id == id);
            if (category == null) return NotFound();

            var viewModel = _mapper.Map<PharmacyCategoryVM>(category);
            ViewBag.currentPage = pageNumber;
            return View(viewModel);
        }

        [HttpGet]
        [Route("Delete")]
        public IActionResult Delete(int id, int pageNumber = 0)
        {
            var category = _unitOfWork.PharmacyCategoryRepository.RetriveItem(e => e.Id == id);
            if (category == null) return NotFound();

            if (!string.IsNullOrEmpty(category.Img))
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Images", "PharmacyCategories", category.Img);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            _unitOfWork.PharmacyCategoryRepository.Delete(category);
            _unitOfWork.Commit();
            
            return RedirectToAction("Index", new { pageNumber });
        }

        [HttpGet]
        [Route("Search")]
        public IActionResult Search(string searchText, int pageNumber = 0)
        {
            var categories = _unitOfWork.PharmacyCategoryRepository
                .Retrive(e => e.Name.ToLower().Contains(searchText.ToLower()))
                .Skip(pageNumber * PageSize)
                .Take(PageSize)
                .ToList();

            var categoriesVM = _mapper.Map<List<PharmacyCategoryVM>>(categories);
            return PartialView("_Search", categoriesVM);
        }
    }
}
