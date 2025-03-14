using AutoMapper;
using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub.PharmacySection;
using CMS.Models.CuraHub.PharmacySection.PharmacySectionVM;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMS.Perestation.Layer.Areas.Admin.Controllers.CuraHub.Pharmacy
{
    [Area("Admin")]
    [Route("Admin/CuraHub/Pharmacy/MedicineManufactory")]
    public class MedicineManufactoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private const int PageSize = 8;

        public MedicineManufactoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
            var manufactories = _unitOfWork.MedicineManufactoryRepository
                .Retrive()
                .Skip(pageNumber * PageSize)
                .Take(PageSize)
                .ToList();

            int totalItems = _unitOfWork.MedicineManufactoryRepository.Retrive().Count();
            SetPaginationData(pageNumber, totalItems);

            var manufactoryVM = _mapper.Map<List<MedicineManufactoryVM>>(manufactories);
            return View(manufactoryVM);
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
        public IActionResult Create(MedicineManufactoryVM manufactoryVM)
        {
            if (ModelState.IsValid)
            {
                var manufactory = _mapper.Map<MedicineManufactory>(manufactoryVM);
                _unitOfWork.MedicineManufactoryRepository.Create(manufactory);
                _unitOfWork.Commit();
                
                int totalItems = _unitOfWork.MedicineManufactoryRepository.Retrive().Count();
                SetPaginationData(0, totalItems);
                return RedirectToAction(nameof(Index), new { pageNumber = ViewBag.lastPage });
            }
            return View(manufactoryVM);
        }

        [HttpGet]
        [Route("Edit")]
        public IActionResult Edit(int id, int pageNumber = 0)
        {
            var manufactory = _unitOfWork.MedicineManufactoryRepository.RetriveItem(m => m.Id == id);
            if (manufactory == null) return NotFound();

            var manufactoryVM = _mapper.Map<MedicineManufactoryVM>(manufactory);
            ViewBag.currentPage = pageNumber;
            return View(manufactoryVM);
        }

        [HttpPost]
        [Route("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MedicineManufactoryVM manufactoryVM, int pageNumber = 0)
        {
            if (ModelState.IsValid)
            {
                var manufactory = _mapper.Map<MedicineManufactory>(manufactoryVM);
                _unitOfWork.MedicineManufactoryRepository.Update(manufactory);
                _unitOfWork.Commit();
                return RedirectToAction(nameof(Index), new { pageNumber });
            }
            ViewBag.currentPage = pageNumber;
            return View(manufactoryVM);
        }

        [HttpGet]
        [Route("Details")]
        public IActionResult Details(int id, int pageNumber = 0)
        {
            var manufactory = _unitOfWork.MedicineManufactoryRepository.RetriveItem(m => m.Id == id);
            if (manufactory == null) return NotFound();

            var manufactoryVM = _mapper.Map<MedicineManufactoryVM>(manufactory);
            ViewBag.currentPage = pageNumber;
            return View(manufactoryVM);
        }

        [HttpGet]
        [Route("Delete")]
        public IActionResult Delete(int id, int pageNumber = 0)
        {
            var manufactory = _unitOfWork.MedicineManufactoryRepository.RetriveItem(m => m.Id == id);
            if (manufactory != null)
            {
                _unitOfWork.MedicineManufactoryRepository.Delete(manufactory);
                _unitOfWork.Commit();
            }
            return RedirectToAction(nameof(Index), new { pageNumber });
        }

        [HttpGet]
        [Route("Search")]
        public IActionResult Search(string searchText, int pageNumber = 0)
        {
            var manufactories = _unitOfWork.MedicineManufactoryRepository
                .Retrive(e => e.Name.ToLower().Contains(searchText.ToLower()))
                .Skip(pageNumber * PageSize)
                .Take(PageSize)
                .ToList();

            var manufactoryVM = _mapper.Map<List<MedicineManufactoryVM>>(manufactories);
            return PartialView("_Search", manufactoryVM);
        }
    }
}
