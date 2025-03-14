using AutoMapper;
using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub.IdentitySection;
using CMS.Models.CuraHub.QuestionAndAnswerSection;
using CMS.Models.CuraHub.QuestionAndAnswerSection.QuestionsAndAnswersVM;
using CMS.Utitlities.StaticData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Perestation.Layer.Areas.Staff.Controllers.CuraHub.Q_A
{
    [Authorize(Roles = ($"{Role.DoctorRole}"))]
    [Area(nameof(Staff))]
    [Route("Staff/CuraHub/Q&A/Question")]
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
        public IActionResult Index(int PageNumber , int SpecializationId , int DoctorId)
        {
            var user = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
            if (user != null)
            {

                var questions = this._unitOfWork.QuestionAndAnswerRepository.Retrive(filter: e=>e.SpecializationId == SpecializationId 
                && e.Status == false , includeProps: [e => e.Specialization]);
                Cust_QuestionAndAnswerVM cust_QuestionAndAnswerVM = new Cust_QuestionAndAnswerVM();

                cust_QuestionAndAnswerVM.TotalQuestionsCount = questions.Count();
                if (PageNumber < 1) PageNumber = 1;

                questions = questions.Skip((PageNumber - 1) * 5).Take(5);
                cust_QuestionAndAnswerVM.CurrentPage = PageNumber;
                cust_QuestionAndAnswerVM.Questions = questions.ToList();
                cust_QuestionAndAnswerVM.DoctorId = DoctorId;

                return View(cust_QuestionAndAnswerVM);
            }
            return RedirectToAction("Login", "Account", new { area = "Identity" });
        }

        [HttpPost]
        [Route("Answer")]
        public IActionResult Answer(QuestionAndAnswer questionAndAnswer)
        {
            ModelState.Remove("Doctor");
            ModelState.Remove("Specialization");
            ModelState.Remove("Status");
            questionAndAnswer.Status = true;

            if (ModelState.IsValid)
            {
                _unitOfWork.QuestionAndAnswerRepository.Update(questionAndAnswer);
                _unitOfWork.Commit();

            }
            return RedirectToAction("Index" , routeValues: new { PageNumber=1 , SpecializationId =questionAndAnswer.SpecializationId , DoctorId = questionAndAnswer.DoctorId }); 

        }

        [Route("DoctorAnswers")]
        public IActionResult DoctorAnswers(int DoctorId, int PageNumber=1)
        {
            var questions = this._unitOfWork.QuestionAndAnswerRepository.Retrive(filter: e => e.DoctorId == DoctorId
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
    }
}
