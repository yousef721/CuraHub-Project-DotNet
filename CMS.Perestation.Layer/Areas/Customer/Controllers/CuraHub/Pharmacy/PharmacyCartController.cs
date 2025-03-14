using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub.IdentitySection;
using CMS.Models.CuraHub.PharmacySection;
using CMS.Models.CuraHub.PharmacySection.PharmacySectionVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using AutoMapper;
using CMS.Models.Enums;
using Stripe;

namespace CMS.Perestation.Layer.Areas.Customer.Controllers.CuraHub.Pharmacy
{
    [Area(nameof(Customer))]
    [Route("Customer/CuraHub/Pharmacy/PharmacyCart")]
    public class PharmacyCartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper mapper;

        public PharmacyCartController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this._unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.userManager = userManager;
            this._configuration = configuration;
        }

        [Route("Index")]
        public ActionResult Index()
        {   
            var userId = userManager.GetUserId(User);
            var medicineInCart = _unitOfWork.PharmacyCartRepository.Retrive(e => e.ApplicationUserId == userId, [e => e.Medicine]).ToList();
            return View(medicineInCart);
        }
        [HttpPost]
        [Route("PlusToCart")]
        public JsonResult PlusToCart(int medicineId)
        {
            var userId = userManager.GetUserId(User);
            var medicine = _unitOfWork.PharmacyCartRepository.RetriveItem(e => e.MedicineId == medicineId);

            if (userId != null)
            {
                if (medicine != null)
                {
                    medicine.count += 1;
                }
                else
                {
                    medicine = new PharmacyCart
                    {
                        MedicineId = medicineId,
                        count = 1,
                        ApplicationUserId = userId
                    };
                    _unitOfWork.PharmacyCartRepository.Create(medicine);
                }
                _unitOfWork.Commit();
            }
            return Json(medicine);
        }

        [HttpPost]
        [Route("MinusFromCart")]
        public JsonResult MinusFromCart(int medicineId)
        {
            var userId = userManager.GetUserId(User);
            var medicine = _unitOfWork.PharmacyCartRepository.RetriveItem(e => e.MedicineId == medicineId);

            if (medicine != null && medicine.count > 1)
            {
                medicine.count -= 1;
                _unitOfWork.Commit();
            }
            return Json(medicine);
        }

        [HttpPost]
        [Route("DeleteFromCart")]
        public JsonResult DeleteFromCart(int medicineId)
        {
            var userId = userManager.GetUserId(User);
            var medicine = _unitOfWork.PharmacyCartRepository.RetriveItem(e => e.MedicineId == medicineId);

            if (medicine != null)
            {
                _unitOfWork.PharmacyCartRepository.Delete(medicine);
                _unitOfWork.Commit();
            }
            return Json(new { success = true });
        }

        [HttpGet]
        [Route("GetCartCount")]
        public JsonResult GetCartCount()
        {
            var userId = userManager.GetUserId(User);
            var medicinesInCart = _unitOfWork.PharmacyCartRepository.Retrive(e => e.ApplicationUserId == userId).ToList();
            int totalItems = medicinesInCart.Sum(item => item.count); 
            return Json(new { count = totalItems });
        }

        [HttpGet]
        [Route("SummaryOrder")]
        public async Task<IActionResult> SummaryOrder()
        {
            var user = await userManager.GetUserAsync(User);
            var customer = new PharmacyCustomerVM();

            if (user != null)
            {
                customer.Email = user.Email;
            }

            return PartialView("_SummaryOrder", customer);
        }

        [HttpPost]
        [Route("SummaryOrder")]
        public async Task<IActionResult> SummaryOrder(PharmacyCustomerVM pharmacyCustomerVM)
        {
            if (ModelState.IsValid)
            {
                var appUser = await userManager.GetUserAsync(User);
                
                if (appUser != null)
                {
                    pharmacyCustomerVM.Email = appUser.Email;
                }
                
                // Save Customer Information
                var customer = mapper.Map<PharmacyCustomer>(pharmacyCustomerVM);
                customer.ApplicationUserId = appUser.Id;
                _unitOfWork.PharmacyCustomerRepository.Create(customer);
                _unitOfWork.Commit();

                // Get Medicines in Cart
                var medicinesInCart = _unitOfWork.PharmacyCartRepository.Retrive(e => e.ApplicationUserId == appUser.Id, [e => e.Medicine]).ToList();
                
                if (!medicinesInCart.Any())
                {
                    return RedirectToAction("Index");
                }

                // Create Order
                var order = new PharmacyOrder
                {
                    PharmacyCustomerId = customer.Id,
                    PharmacyDeliveryRepresentativeId = null,
                    DateTime = DateTime.Now,
                    Discount = 0, // Adjust if needed
                    TotalPrice = medicinesInCart.Sum(m => m.Medicine.Price * m.count),
                    ShipmentStatus = CMS.Models.Enums.ShipmentStatus.Pending,
                    Quentity = medicinesInCart.Sum(m => m.count)
                };
                _unitOfWork.PharmacyOrderRepository.Create(order);
                _unitOfWork.Commit();

                // Save Medicine Orders
                foreach (var medicine in medicinesInCart)
                {
                    var medicineOrder = new MedicineOrder
                    {
                        MedicineId = medicine.MedicineId,
                        MedicineCount = medicine.count,
                        PharmacyOrderId = order.Id
                    };
                    _unitOfWork.MedicineOrderRepository.Create(medicineOrder);
                }
                _unitOfWork.Commit();

                // Redirect to Payment
                return RedirectToAction("Pay", new { orderId = order.Id });
            }

            return View(pharmacyCustomerVM);
        }

        [Route("Pay")]
        public IActionResult Pay(int orderId)
        {
            var order = _unitOfWork.PharmacyOrderRepository.RetriveItem(e => e.Id == orderId);
            var medicinesInOrder = _unitOfWork.MedicineOrderRepository.Retrive(e => e.PharmacyOrderId == orderId, [e => e.Medicine]).ToList();
            
            if (order == null || !medicinesInOrder.Any())
            {
                return RedirectToAction("Index");
            }
            decimal totalPrice = (decimal)medicinesInOrder.Sum(mo => mo.Medicine.Price * mo.MedicineCount);
            
            // Stripe requires at least 200 fils (2 AED), check the total
            decimal minStripeAmount = 2.00m;  // AED minimum
            decimal egpToAedRate = 0.073m; // Example exchange rate, update accordingly

            if ((totalPrice * egpToAedRate) < minStripeAmount)
            {
                TempData["Error"] = "The total order amount is too low to process a payment.";
                return RedirectToAction("Index");
            }

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = $"{Request.Scheme}://{Request.Host}/Customer/CuraHub/Pharmacy/PharmacyCart/PaymentSuccess?orderId={order.Id}&session_id={{CHECKOUT_SESSION_ID}}",
                CancelUrl = $"{Request.Scheme}://{Request.Host}/Customer/CuraHub/Pharmacy/PharmacyCart/PaymentFailed?orderId={order.Id}",
            };

            foreach (var medicineOrder in medicinesInOrder)
            {
                options.LineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "egp",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = medicineOrder.Medicine.Name,
                        },
                        UnitAmount = (long)(medicineOrder.Medicine.Price * 100),
                    },
                    Quantity = medicineOrder.MedicineCount,
                });
            }

            var service = new SessionService();
            var session = service.Create(options);

            return Redirect(session.Url);
        }

        [Route("PaymentSuccess")]
        public IActionResult PaymentSuccess(int orderId, string session_id)
        {
            var order = _unitOfWork.PharmacyOrderRepository.RetriveItem(e => e.Id == orderId);
            if (order == null)
            {
                TempData["Error"] = "Order not found.";
                return RedirectToAction("Index");
            }

            try
            {
                StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"]; // Ensure you have your Stripe secret key

                // Retrieve the Stripe session using the session_id
                var sessionService = new SessionService();
                var session = sessionService.Get(session_id);

                if (string.IsNullOrEmpty(session.PaymentIntentId))
                {
                    TempData["Error"] = "Payment incomplete or failed.";
                    return RedirectToAction("Index");
                }

                // Retrieve the PaymentIntent to get the charge details
                var paymentIntentService = new PaymentIntentService();
                var paymentIntent = paymentIntentService.Get(session.PaymentIntentId);

                // Get the associated Charge
                var chargeService = new ChargeService();
                var chargeList = chargeService.List(new ChargeListOptions { PaymentIntent = paymentIntent.Id });

                if (chargeList.Data.Count > 0)
                {
                    var charge = chargeList.Data.First();
                    order.TransactionId = charge.Id; // Store the charge ID in the order for future reference
                }
                else
                {
                    TempData["Error"] = "Payment charge not found.";
                    return RedirectToAction("Index");
                }

                // Update order status to Paid
                order.ShipmentStatus = ShipmentStatus.Paid;
                _unitOfWork.PharmacyOrderRepository.Update(order);
                _unitOfWork.Commit();

                // Clear User Cart after Successful Payment
                var userId = userManager.GetUserId(User);
                var cartItems = _unitOfWork.PharmacyCartRepository.Retrive(e => e.ApplicationUserId == userId).ToList();
                foreach (var item in cartItems)
                {
                    _unitOfWork.PharmacyCartRepository.Delete(item);
                }
                _unitOfWork.Commit();

                return RedirectToAction("OrderConfirmation", new { orderId = order.Id });
            }
            catch (StripeException ex)
            {
                TempData["Error"] = $"Stripe Error: {ex.Message}";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Unexpected Error: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [Route("PaymentFailed")]
        public IActionResult PaymentFailed(int orderId)
        {
            ViewBag.OrderId = orderId;
            TempData["Error"] = "Your payment was unsuccessful. Please try again.";
            return View();
        }

        [Route("OrderConfirmation")]
        public IActionResult OrderConfirmation(int orderId)
        {            
            var userId = userManager.GetUserId(User);
            var pharmacyOrder = _unitOfWork.PharmacyOrderRepository.RetriveItem(e => e.Id == orderId && e.PharmacyCustomer.ApplicationUserId == userId, [e=> e.PharmacyCustomer]);
            var medicineOrder = _unitOfWork.MedicineOrderRepository.Retrive(e => e.PharmacyOrderId == orderId, [e => e.Medicine]).ToList();

            MedicinesInOrderVM order = new MedicinesInOrderVM() {
                MedicineOrder = medicineOrder,
                PharmacyOrder = pharmacyOrder
            };

            if (order == null)
            {
                return RedirectToAction("Index");
            }
            
            return View(order);
        }
    }
}
