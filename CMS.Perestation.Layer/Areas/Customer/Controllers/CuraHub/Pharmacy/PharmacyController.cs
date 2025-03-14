using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub.IdentitySection;
using CMS.Models.CuraHub.PharmacySection.PharmacySectionVM;
using CMS.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Linq;

namespace CMS.Perestation.Layer.Areas.Customer.Controllers.CuraHub.Pharmacy
{
    [Area(nameof(Customer))]
    [Route("Customer/CuraHub/Pharmacy")]
    public class PharmacyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> manager;

        public PharmacyController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> manager)
        {
            this._unitOfWork = unitOfWork;
            this.manager = manager;
        }

        [Route("Index")]
        public IActionResult Index()
        {
            var medicineCategories = _unitOfWork.PharmacyCategoryRepository.Retrive().ToList();
            return View(medicineCategories);
        }

        [HttpGet]
        [Route("SearchMedicines")]
        public IActionResult SearchMedicines(string searchText)
        {
            var medicines = _unitOfWork.MedicineRepository.Retrive(m => m.Name.Contains(searchText)).ToList();

            return PartialView("_SearchMedicineList", medicines);
        }

        [HttpGet]
        [Route("UserOrders")]
        public IActionResult UserOrders()
        {
            var userId = manager.GetUserId(User);
            var orderCustomer = _unitOfWork.PharmacyCustomerRepository.Retrive(m => m.ApplicationUserId == userId).ToList();
           
            return View(orderCustomer);
        }
        
        [HttpGet]
        [Route("OrderDetails")]
        public IActionResult OrderDetails(int customerId)
        {
            var order = _unitOfWork.PharmacyOrderRepository.RetriveItem(c => c.PharmacyCustomerId == customerId, [e => e.PharmacyCustomer]);
            var medicinesOrder = _unitOfWork.MedicineOrderRepository.Retrive(e => e.PharmacyOrderId == order.Id, [e => e.Medicine]).ToList();

            MedicinesInOrderVM medicinesInOrder = new MedicinesInOrderVM()
            {
                MedicineOrder = medicinesOrder,
                PharmacyOrder = order
            };

            return View(medicinesInOrder);
        }

        [HttpGet]
        [Route("RequestCancel")]
        public IActionResult RequestCancel(int Id)
        {
            var order = _unitOfWork.PharmacyOrderRepository.RetriveItem(e => e.Id == Id);
            if (order == null) return NotFound();
            if (order.ShipmentStatus == ShipmentStatus.Delivered)
            {
                TempData["Error"] = "Delivered orders cannot be canceled.";
                return RedirectToAction("UserOrders");
            }

            order.ShipmentStatus = ShipmentStatus.RequestCancel;
            _unitOfWork.PharmacyOrderRepository.Update(order);
            _unitOfWork.Commit();

            return RedirectToAction("UserOrders");
        }
    }
}
