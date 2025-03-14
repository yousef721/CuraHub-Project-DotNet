using AutoMapper;
using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub.IdentitySection;
using CMS.Utitlities.StaticData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace CMS.Perestation.Layer.Areas.Admin.Controllers.CuraHub
{
    [Area(nameof(Admin))]
    [Authorize(Roles = ($"{Role.AdminRole}"))]
    [Route("Admin/CuraHub/Message")]
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

        [Route("Index")]
        public IActionResult Index()
        {
            var messages = this._unitOfWork.MessageRepository.Retrive();
            return View(messages.ToList());
        }

        [Route("Delete")]
        public IActionResult Delete(int MessageId)
        {
            var message = _unitOfWork.MessageRepository.RetriveItem(filter : e =>e.Id == MessageId);
            if (message != null)
            {
                _unitOfWork.MessageRepository.Delete(message);
                _unitOfWork.Commit();
            }
            return RedirectToAction("Index");
        }
    }
}
