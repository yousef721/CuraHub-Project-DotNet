using AutoMapper;
using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub.IdentitySection;
using CMS.Models.CuraHub.QuestionAndAnswerSection.QuestionsAndAnswersVM;
using CMS.Utitlities.StaticData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Perestation.Layer.Areas.Admin.Controllers.CuraHub.Q_A
{

    [Authorize(Roles = ($"{Role.AdminRole}"))]
    [Area(nameof(Admin))]
    [Route("Admin/CuraHub/Q&A/Question")]
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
        public IActionResult Index(int PageNumber=1, int? SpecializationId=null, int? DoctorId=null)
        {
            var questions = _unitOfWork.QuestionAndAnswerRepository.Retrive(
                includeProps: [e =>e.Doctor , e =>e.Specialization]);

            if(SpecializationId != null)
            {
                questions = questions.Where(e => e.SpecializationId == SpecializationId);
            }
            if (DoctorId != null)
            {
                questions = questions.Where(e => e.DoctorId == DoctorId);
            }
            if(PageNumber <1) { PageNumber = 1; }

            Admin_QuestionsVM admin_QuestionsVM = new Admin_QuestionsVM();

            admin_QuestionsVM.PageNumber = PageNumber;
            admin_QuestionsVM.TotalQuestionCount = questions.Count();

            admin_QuestionsVM.Specializations = _unitOfWork.SpecializationRepository.Retrive().ToList();
            admin_QuestionsVM.Doctors = _unitOfWork.DoctorRepository.Retrive().ToList();

            questions = questions.Skip((PageNumber - 1) * 5).Take(5);
            admin_QuestionsVM.QuestionAndAnswers = questions.ToList();
            return View(admin_QuestionsVM);
        }
        [Route("Delete")]
        public IActionResult Delete(int QuestionId)
        {
            var question = _unitOfWork.QuestionAndAnswerRepository.RetriveItem(filter: e => e.Id == QuestionId);
            if (question != null)
            {
                _unitOfWork.QuestionAndAnswerRepository.Delete(question);
                _unitOfWork.Commit();
            }
            return RedirectToAction("Index");
        }
    }
}
