using AutoMapper;
using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub.ClinicSection;
using CMS.Models.CuraHub.ClinicSection.ClinicSectionVM;
using CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.RequestDoctorSectionVM;
using CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.ScheduleVM;
using CMS.Models.CuraHub.IdentitySection;
using CMS.Utitlities.Helper;
using CMS.Utitlities.StaticData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Perestation.Layer.Areas.Admin.Controllers.CuraHub.Clinic
{
    [Area(nameof(Admin))]
    [Authorize(Roles = ($"{Role.AdminRole}"))]
    [Route("Admin/CuraHub/Clinic/RequestDoctor")]
    public class RequestDoctorController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;


        public RequestDoctorController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        [Route("Index")]
        public IActionResult Index(string? query = null, int PageNumber = 1, int? SpecializationId = null, string? Title = null)
        {
            var requestDoctors = this._unitOfWork.RequestDoctorRepository.Retrive(includeProps:[e =>e.Specialization]);
            if (query != null)
            {
                query = query.Trim();

                requestDoctors = requestDoctors.Where(e => e.FirstName.Contains(query) || e.LastName.Contains(query) || e.PersonalNationalIDNumber.Contains(query));
            }

            if (SpecializationId != null)
            {

                requestDoctors = requestDoctors.Where(e => e.SpecializationId == SpecializationId);
            }
            if (Title != null)
            {
                Title = Title.Trim();

                requestDoctors = requestDoctors.Where(e => e.Title.Contains(Title));
            }
            Admin_RequestDoctorsVM admin_RequestDoctorsVM = new Admin_RequestDoctorsVM();
            if (PageNumber < 1) PageNumber = 1;
            admin_RequestDoctorsVM.TotalRequestDoctorCount = requestDoctors.Count();
            admin_RequestDoctorsVM.CurrentPageNumber = PageNumber;
            requestDoctors = requestDoctors.Skip((PageNumber - 1) * 5).Take(5);
            admin_RequestDoctorsVM.Specializations = this._unitOfWork.SpecializationRepository.Retrive().ToList();
            admin_RequestDoctorsVM.RequestDoctors = requestDoctors.ToList();
            return View(admin_RequestDoctorsVM);
        }

        [Route("Details")]
        public IActionResult Details(int RequestDoctorId)
        {
            var requestDoctor = this._unitOfWork.RequestDoctorRepository.RetriveItem(filter: e => e.Id == RequestDoctorId, includeProps: [e => e.Specialization]) as RequestDoctor;
            if (requestDoctor != null)
            {

                var RequestDoctorDetailsVM = this._mapper.Map<RequestDoctorDetailsVM>(requestDoctor);
                //doctorDetailsVM.Schedules = this._unitOfWork.ScheduleRepository.Retrive(filter: e => e.DoctorId == doctorDetailsVM.Id).ToList();
                //doctorDetailsVM.ClinicReceptionists = this._unitOfWork.ClinicReceptionistRepository.Retrive(filter: e => e.DoctorId == doctorDetailsVM.Id).ToList();
                //doctorDetailsVM.qualifications = this._unitOfWork.QualificationRepository.Retrive(filter: e => e.DoctorId == doctorDetailsVM.Id).ToList();
                //doctorDetailsVM.QuestionAndAnswers = this._unitOfWork.QuestionAndAnswerRepository.Retrive(filter: e => e.DoctorId == doctorDetailsVM.Id).ToList();
                //doctorDetailsVM.RequestClinicReceptionists = this._unitOfWork.RequestClinicReceptionistRepository.Retrive(filter: e => e.DoctorId == doctorDetailsVM.Id).ToList();

                return View(RequestDoctorDetailsVM);
            }
            return RedirectToAction("Index");
        }


        [Route("Accept")]
        public  IActionResult Accept(int RequestDoctorId)
        {
            var request = _unitOfWork.RequestDoctorRepository.RetriveItem(filter: e => e.Id == RequestDoctorId) as RequestDoctor;
            if (request != null)
            {
                var doctor = _mapper.Map<Doctor>(request);
                doctor.Id = 0;
                _unitOfWork.DoctorRepository.Create(doctor);
                _unitOfWork.RequestDoctorRepository.Delete(request);
                _unitOfWork.Commit();

                var savedDoctor = _unitOfWork.DoctorRepository.RetriveItem(filter: e => e.PersonalNationalIDNumber == doctor.PersonalNationalIDNumber);



                CreateDoctorScheduleVM createDoctorScheduleVM = new CreateDoctorScheduleVM();
                if (savedDoctor != null)
                {
                    // Change Role to Doctor
                    var user = _userManager.FindByIdAsync(savedDoctor.ApplicationUserId).GetAwaiter().GetResult();
                    if (user != null)
                    {
                        _userManager.RemoveFromRolesAsync(user, _userManager.GetRolesAsync(user).GetAwaiter().GetResult()).GetAwaiter().GetResult();
                        _userManager.AddToRoleAsync(user, Role.DoctorRole).GetAwaiter().GetResult();
                    }

                    createDoctorScheduleVM.DoctorId = savedDoctor.Id;
                    TempData["DoctorId"] = savedDoctor.Id;

                }
                return RedirectToAction("CreateDoctorSchedule", controllerName: "Schedule", createDoctorScheduleVM);


            }
            return RedirectToAction(nameof(Index));

        }
        [Route("Reject")]
        public IActionResult Reject(int RequestDoctorId)
        {
            var request = _unitOfWork.RequestDoctorRepository.RetriveItem(filter: e => e.Id == RequestDoctorId) as RequestDoctor;
            if(request != null)
            {
                if(request.ProfilePicture != "\"doctorDefault.webp\"")
                {
                    FileOperation.DeleteFile(request.ProfilePicture, "Images\\DoctorsPictures");

                }

                _unitOfWork.RequestDoctorRepository.Delete(request);
                _unitOfWork.Commit();
            }
            return RedirectToAction(nameof(Index));
        }



    }
}
