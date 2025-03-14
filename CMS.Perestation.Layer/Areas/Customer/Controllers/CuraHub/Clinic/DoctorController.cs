using AutoMapper;
using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub.ClinicSection;
using CMS.Models.CuraHub.ClinicSection.ClinicSectionVM;
using CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.ForCustomerSection.DoctorVMSection;
using CMS.Models.CuraHub.IdentitySection;
using CMS.Utitlities.StaticData;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMS.Perestation.Layer.Areas.Customer.Controllers.CuraHub.Clinic
{
    [Area(nameof(Customer))]
    [Route("Customer/CuraHub/Clinic/Doctor")]
    public class DoctorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;


        public DoctorController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._userManager = userManager;
        }
        [Route("Index")]
        public IActionResult Index(string? query = null, int PageNumber = 1, int? SpecializationId = null, string? Title = null, string? State = null, string? City = null, int? Rate = null, int? ConsultationFees = null ,string? Gender=null)
        {
           

            var doctors = _unitOfWork.DoctorRepository.Retrive(includeProps: [e => e.Specialization]);
            Cust_DoctorsVM cust_DoctorsVM = new Cust_DoctorsVM();

            if (query != null)
            {
                query = query.Trim();

                doctors = doctors.Where(e => e.FirstName.Contains(query) || e.LastName.Contains(query) || e.PersonalNationalIDNumber.Contains(query));
                cust_DoctorsVM.query = query;
            }
            if (Gender != null)
            {
                Gender = Gender.Trim();

                doctors = doctors.Where(e => e.Gender.Contains(Gender));
                cust_DoctorsVM.Gender = Gender;
            }

            if (SpecializationId != null)
            {

                doctors = doctors.Where(e => e.SpecializationId == SpecializationId);
                cust_DoctorsVM.SpecializationId = SpecializationId;
            }
            if (Title != null)
            {
                Title = Title.Trim();

                doctors = doctors.Where(e => e.Title.Contains(Title));
                cust_DoctorsVM.Title = Title;
            }
            if (State != null)
            {
                State = State.Trim();

                doctors = doctors.Where(e => e.State.Contains(State));
                cust_DoctorsVM.State = State;
            }
            if (City != null)
            {
                City = City.Trim();

                doctors = doctors.Where(e => e.City.Contains(City));
                cust_DoctorsVM.City = City;
            }

            if (Rate != null)
            {
                doctors = doctors.Where(e => ((e.Rate >= Rate)));
                cust_DoctorsVM.Rate = Rate;
            }
            if (ConsultationFees != null)
            {
                cust_DoctorsVM.ConsultationFees = ConsultationFees;
              
                doctors = doctors.Where(e => ((e.ConsultationFees <= ConsultationFees)));

                
            }

            cust_DoctorsVM.TotalDoctorCount = doctors.Count();
            if (PageNumber < 1) PageNumber = 1;
            cust_DoctorsVM.CurrentPage = PageNumber;
            
            doctors = doctors.Skip((PageNumber - 1) * 5).Take(5);


            cust_DoctorsVM.Doctors = doctors.ToList();
            cust_DoctorsVM.Specializations = this._unitOfWork.SpecializationRepository.Retrive().ToList();
            cust_DoctorsVM.Schedules = this._unitOfWork.ScheduleRepository.Retrive().ToList();




            var appointments = this._unitOfWork.PatientAppointmentRepository.Retrive(includeProps: [e =>e.Schedule]).ToList();

            foreach (var appointment in appointments)
            {
                if (DateOnly.FromDateTime(DateTime.Now) > (appointment.date))
                {
                    appointment.Schedule.Available = true;
                    if (DateOnly.FromDateTime(DateTime.Now.AddDays(3)) > (appointment.date))
                    {
                        _unitOfWork.PatientAppointmentRepository.Delete(appointment);
                    }
                }
            }
            _unitOfWork.Commit();






            
            return View(cust_DoctorsVM);
        }

        [Route("Search")]
        public IActionResult Search(string? query = null, int PageNumber = 1, int? SpecializationId = null, string? Title = null, string? State = null, string? City = null, int? Rate = null, int? ConsultationFees = null, string? Gender = null)
        {


            var doctors = _unitOfWork.DoctorRepository.Retrive(includeProps: [e => e.Specialization]);
            Cust_DoctorsVM cust_DoctorsVM = new Cust_DoctorsVM();

            if (query != null)
            {
                query = query.Trim();

                doctors = doctors.Where(e => e.FirstName.Contains(query) || e.LastName.Contains(query) || e.PersonalNationalIDNumber.Contains(query));
                cust_DoctorsVM.query = query;
            }
            if (Gender != null)
            {
                Gender = Gender.Trim();

                doctors = doctors.Where(e => e.Gender.Contains(Gender));
                cust_DoctorsVM.Gender = Gender;
            }

            if (SpecializationId != null)
            {

                doctors = doctors.Where(e => e.SpecializationId == SpecializationId);
                cust_DoctorsVM.SpecializationId = SpecializationId;
            }
            if (Title != null)
            {
                Title = Title.Trim();

                doctors = doctors.Where(e => e.Title.Contains(Title));
                cust_DoctorsVM.Title = Title;
            }
            if (State != null)
            {
                State = State.Trim();

                doctors = doctors.Where(e => e.State.Contains(State));
                cust_DoctorsVM.State = State;
            }
            if (City != null)
            {
                City = City.Trim();

                doctors = doctors.Where(e => e.City.Contains(City));
                cust_DoctorsVM.City = City;
            }

            if (Rate != null)
            {
                doctors = doctors.Where(e => ((e.Rate >= Rate)));
                cust_DoctorsVM.Rate = Rate;
            }
            if (ConsultationFees != null)
            {
                cust_DoctorsVM.ConsultationFees = ConsultationFees;

                doctors = doctors.Where(e => ((e.ConsultationFees <= ConsultationFees)));


            }

            cust_DoctorsVM.TotalDoctorCount = doctors.Count();
            if (PageNumber < 1) PageNumber = 1;
            cust_DoctorsVM.CurrentPage = PageNumber;

            doctors = doctors.Skip((PageNumber - 1) * 5).Take(5);


            cust_DoctorsVM.Doctors = doctors.ToList();
            cust_DoctorsVM.Specializations = this._unitOfWork.SpecializationRepository.Retrive().ToList();
            cust_DoctorsVM.Schedules = this._unitOfWork.ScheduleRepository.Retrive().ToList();




            var appointments = this._unitOfWork.PatientAppointmentRepository.Retrive(includeProps: [e => e.Schedule]).ToList();

            foreach (var appointment in appointments)
            {
                if (DateOnly.FromDateTime(DateTime.Now) > (appointment.date))
                {
                    appointment.Schedule.Available = true;
                    if (DateOnly.FromDateTime(DateTime.Now.AddDays(3)) > (appointment.date))
                    {
                        _unitOfWork.PatientAppointmentRepository.Delete(appointment);
                    }
                }
            }
            _unitOfWork.Commit();

            return PartialView( "_DoctorPartialView", cust_DoctorsVM);
        }


        [Route("Details")]
        public IActionResult Details(int DoctorId, bool DetailsForUser = false)
        {

            var doctor = this._unitOfWork.DoctorRepository.RetriveItem(filter : e =>e.Id == DoctorId, includeProps: [e =>e.Specialization] ) as Doctor;
            if (doctor != null)
            {
                Cust_DoctorDetailsVM cust_DoctorDetailsVM = new Cust_DoctorDetailsVM();
                cust_DoctorDetailsVM.Doctor = doctor;
                cust_DoctorDetailsVM.Schedules = _unitOfWork.ScheduleRepository.Retrive(filter: e => e.DoctorId == DoctorId).ToList();
                cust_DoctorDetailsVM.DoctorReviews = this._unitOfWork.DoctorReviewRepository.Retrive(filter: e => e.DoctorId == DoctorId,
                includeProps: [e => e.Doctor, e => e.ApplicationUser]).ToList();

                var appointments = _unitOfWork.PatientAppointmentRepository.Retrive(filter:e =>e.Schedule.DoctorId 
                == DoctorId ).Include(e =>e.Schedule).ThenInclude(e =>e.Doctor);

                foreach ( var appointment in appointments )
                {
                    var nowTime = DateOnly.FromDateTime(DateTime.Now);
                    var date  = appointment.date;
                    if (date < nowTime)
                    {
                        appointment.Schedule.Available = true;
                    }
                }
                _unitOfWork.Commit();
                cust_DoctorDetailsVM.DetailsForUser = DetailsForUser;
                return View(cust_DoctorDetailsVM);
            }




            return RedirectToAction("Index", "Home");
        }

        [Route("SubmitRating")]
        public IActionResult SubmitRating(int DoctorId , int rating)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var doctor = this._unitOfWork.DoctorRepository.RetriveItem(filter: e => e.Id == DoctorId);
                if (doctor != null)
                {
                    doctor.Rate = ((doctor.Rate * doctor.userRatingCount++) + rating) /(doctor.userRatingCount);
                    _unitOfWork.DoctorRepository.Update(doctor);
                    _unitOfWork.Commit();
                }
                return RedirectToAction(nameof(Details), routeValues: new { DoctorId });
            }
            return RedirectToAction("Login", "Account" , new { area ="Identity"});
        }
    }
}
