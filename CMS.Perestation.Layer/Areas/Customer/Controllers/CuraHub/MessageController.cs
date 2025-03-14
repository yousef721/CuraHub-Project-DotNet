using AutoMapper;
using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub;
using CMS.Models.CuraHub.IdentitySection;
using CMS.Utitlities.StaticData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace CMS.Perestation.Layer.Areas.Customer.Controllers.CuraHub
{
    [Area(nameof(Customer))]
    [Route("Customer/CuraHub/Message")]
    public class MessageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;


        public MessageController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._userManager = userManager;
        }
        [HttpPost]
        [Route("Create")]
        public IActionResult Create(Message message)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.MessageRepository.Create(message);
                _unitOfWork.Commit();
                return RedirectToAction("Index", "Home", new { area = "Customer" });
            }
            return View(message);
        }
    }
}
