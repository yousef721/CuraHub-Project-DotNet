using AutoMapper;
using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub.ClinicSection;
using CMS.Models.CuraHub.IdentitySection;
using CMS.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;

namespace CMS.Perestation.Layer.Areas.Customer.Controllers.CuraHub.Clinic
{
    [Area(nameof(Customer))]
    [Route("Customer/CuraHub/Clinic/PatientAppointmentCard")]

    public class PatientAppointmentCardController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;


        public PatientAppointmentCardController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._userManager = userManager;
        }
        //Customer/CuraHub/Clinic/PatientAppointmentCard/Index
        [Route("Index")]
        public IActionResult Index()
        {
            var carts = this._unitOfWork.PatientAppointmentCardRepository.Retrive(filter: e => e.ApplicationUserId == _userManager.GetUserId(User), includeProps: [e => e.ApplicationUser])
                .Include(e => e.PatientAppointment).ThenInclude(patientAppointment => patientAppointment.Schedule).ThenInclude(schedule => schedule.Doctor).ToList();

            return View(carts);
        }
        // Customer/CuraHub/Clinic/PatientAppointmentCard/PayOption
        [HttpGet]
        [Route("PayOption")]
        public IActionResult PayOption(PatientAppointment patientAppointment)
        {
            return View(patientAppointment);
        }
        [HttpPost]
        [Route("PayOption")]
        public IActionResult PayOption(PatientAppointment patientAppointment, PaymentOptions payment)
        {
            ModelState.Remove("Patient");
            ModelState.Remove("Schedule");
            ModelState.Remove("paid");
            if (ModelState.IsValid) 
            {
                if (payment == PaymentOptions.CreditCard)
                {
                    _unitOfWork.PatientAppointmentCardRepository.Create(new PatientAppointmentCard
                    {
                        ApplicationUserId = _userManager.GetUserId(User)??"",
                        PatientId = patientAppointment.PatientId,
                        ScheduleId = patientAppointment.ScheduleId
                    });
                    _unitOfWork.Commit();
                    TempData["PatientId"] = patientAppointment.PatientId;
                    TempData["ScheduleId"] = patientAppointment.ScheduleId;

                    return RedirectToAction("Pay", "PatientAppointmentCard");
                }
                else
                {
                    TempData["SuccessMessage"] = "The operation was successful!";

                    return RedirectToAction("UserAppoitments", "PatientAppointment", routeValues: _userManager.GetUserId(User));
                }
            }
            return RedirectToAction("Index");
        }
        // Customer/CuraHub/Clinic/PatientAppointmentCard/Pay
        [Route("Pay")]
        public IActionResult Pay()
        {
            PatientAppointment patientAppointment = new PatientAppointment
            {
                ScheduleId = (int)TempData["ScheduleId"],
                PatientId = (int)TempData["PatientId"]
            };
            var carts = this._unitOfWork.PatientAppointmentCardRepository.Retrive(filter: e => e.ApplicationUserId == _userManager.GetUserId(User) && e.PatientId == patientAppointment.PatientId && e.ScheduleId == patientAppointment.ScheduleId , includeProps: [e => e.ApplicationUser])
                .Include(e => e.PatientAppointment).ThenInclude(patientAppointment => patientAppointment.Schedule).ThenInclude(schedule => schedule.Doctor).ToList();

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = $"{Request.Scheme}://{Request.Host}/Customer/CuraHub/Clinic/PatientAppointmentCard/Success?PatientId={carts.FirstOrDefault()?.PatientId}&ScheduleId={carts.FirstOrDefault()?.ScheduleId}",
                CancelUrl = $"{Request.Scheme}://{Request.Host}/Customer/CuraHub/Clinic/PatientAppointmentCard/Cancel",
            };

           
            foreach (var item in carts)
            {
                options.LineItems.Add(
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.PatientAppointment.Schedule.Doctor.FirstName+ item.PatientAppointment.Schedule.Doctor.LastName,//model.ProductName,
                                Description = item.PatientAppointment.Schedule.Doctor.Title+" Doctor ",
                            },
                            UnitAmount = (long)item.PatientAppointment.Schedule.Doctor.ConsultationFees * 100,
                        },
                        Quantity = 1,
                    });
            }
            var service = new SessionService();
            var session = service.Create(options);
            return Redirect(session.Url);
        }

        [Route("Cancel")]
        public IActionResult Cancel()
        {
            return View();
        }
        // Customer/CuraHub/Clinic/PatientAppointmentCard/Success?ScheduleId=&PatientId=
        [Route("Success")]
        public IActionResult Success(int ScheduleId , int PatientId)
        {
            //Customer/CuraHub/Clinic/PatientAppointment/UserAppoitments
            var appointment = this._unitOfWork.PatientAppointmentRepository.RetriveItem(filter: e => e.PatientId == PatientId && e.ScheduleId == ScheduleId);
            if (appointment != null)
            {
                appointment.paid = true;
                _unitOfWork.PatientAppointmentRepository.Update(appointment);
                _unitOfWork.Commit();
            }
            TempData["SuccessMessage"] = "The operation was successful!";

            return RedirectToAction("UserAppoitments", "PatientAppointment");
        }

        [Route("CompletePayment")]
        public IActionResult CompletePayment(int ScheduleId, int PatientId ,int ClinicReceptionistId)
        {
            //Customer/CuraHub/Clinic/PatientAppointment/UserAppoitments
            var appointment = this._unitOfWork.PatientAppointmentRepository.RetriveItem(filter: e => e.PatientId == PatientId && e.ScheduleId == ScheduleId);
            if (appointment != null)
            {
                appointment.paid = true;
                _unitOfWork.PatientAppointmentRepository.Update(appointment);
                _unitOfWork.Commit();
                TempData["SuccessMessage"] = "The operation was successful!";

                return RedirectToAction("ShowAppoitments", "PatientAppointment", routeValues: new {appointment.date , ClinicReceptionistId });
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
