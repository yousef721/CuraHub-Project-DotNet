using AutoMapper;
using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub.IdentitySection;
using CMS.Models.CuraHub.QuestionAndAnswerSection;
using CMS.Models.CuraHub.QuestionAndAnswerSection.QuestionsAndAnswersVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Perestation.Layer.Areas.Customer.Controllers.CuraHub.Q_A
{
    [Authorize]
    [Area(nameof(Customer))]
    [Route("Customer/CuraHub/Q&A/Question")]
    public class QuestionController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public QuestionController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._userManager = userManager;

        }


        [Route("Index")]
        public IActionResult Index(int PageNumber = 1)
        {
            var user = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
            if (user != null)
            {
                var questions = this._unitOfWork.QuestionAndAnswerRepository.Retrive(filter: e => e.ApplicationUserId == user.Id
                , includeProps: [e => e.Specialization, e => e.Doctor]
                );
                Cust_QuestionAndAnswerVM cust_QuestionAndAnswerVM = new Cust_QuestionAndAnswerVM();

                cust_QuestionAndAnswerVM.TotalQuestionsCount = questions.Count();
                if (PageNumber < 1) PageNumber = 1;

                questions = questions.Skip((PageNumber - 1) * 5).Take(5);
                cust_QuestionAndAnswerVM.CurrentPage = PageNumber;
                cust_QuestionAndAnswerVM.Questions = questions.ToList();

                return View(cust_QuestionAndAnswerVM);
            }
            return RedirectToAction("Login", "Account", new { area = "Identity" });
        }

        [Route("Create")]
        [HttpGet]
        public IActionResult Create()
        {
            Cust_QuestionAndAnswerCreateVM cust_QuestionAndAnswerCreateVM = new Cust_QuestionAndAnswerCreateVM();
            cust_QuestionAndAnswerCreateVM.Specializations = _unitOfWork.SpecializationRepository.Retrive().ToList();
            return View(cust_QuestionAndAnswerCreateVM);
        }
        [Route("Create")]
        [HttpPost]
        public IActionResult Create(int SpecializationId, string Question)
        {
            var user = _userManager.GetUserAsync(User).GetAwaiter().GetResult();

            if (user != null)
            {
                QuestionAndAnswer questionAndAnswer = new QuestionAndAnswer();
                questionAndAnswer.ApplicationUserId = user.Id;
                questionAndAnswer.Question = Question;
                questionAndAnswer.SpecializationId = SpecializationId;
                questionAndAnswer.Status = false;
                questionAndAnswer.Date = DateTime.Now;
                questionAndAnswer.Answer = "";

                _unitOfWork.QuestionAndAnswerRepository.Create(questionAndAnswer);
                _unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Login", "Account", new { area = "Identity" });
        }
    }
}

