using AutoMapper;
using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub.ClinicSection;
using CMS.Models.CuraHub.ClinicSection.ClinicSectionVM;
using CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.ScheduleVM;
using CMS.Models.CuraHub.IdentitySection;
using CMS.Models.CuraHub.IdentitySection.IdentitySectionVM;
using CMS.Models.CuraHub.QuestionAndAnswerSection;
using CMS.Models.Enums;
using CMS.Utitlities.Helper;
using CMS.Utitlities.StaticData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Mono.TextTemplating;
using System.Data;
using System.IO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CMS.Perestation.Layer.Areas.Admin.Controllers.CuraHub.Clinic
{
    [Area(nameof(Admin))]
    [Authorize(Roles = ($"{Role.AdminRole}"))]
    [Route("Admin/CuraHub/Clinic/Doctor")]
    public class DoctorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager; 


        public DoctorController(IUnitOfWork unitOfWork , IMapper mapper , UserManager<ApplicationUser> userManager)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._userManager = userManager;
        }
        [Route("Index")]
        public IActionResult Index(string? query = null , int PageNumber = 1, int? SpecializationId=null, string? Title = null, string? State = null, string? City = null, int? Rate = null, int? ConsultationFees = null)
        {

            var doctors = _unitOfWork.DoctorRepository.Retrive(includeProps: [e => e.Specialization]);
           
            if (query != null)
            {
                query = query.Trim();

                doctors = doctors.Where(e => e.FirstName.Contains(query) || e.LastName.Contains(query) || e.PersonalNationalIDNumber.Contains(query));
            }

            if (SpecializationId != null)
            {

                doctors = doctors.Where(e=>e.SpecializationId == SpecializationId);
            }
            if (Title != null)
            {
                Title = Title.Trim();

                doctors = doctors.Where(e => e.Title.Contains(Title));
            }
            if (State != null)
            {
                State = State.Trim();

                doctors = doctors.Where(e => e.State.Contains(State));
            }
            if (City != null)
            {
                City = City.Trim();

                doctors = doctors.Where(e => e.City.Contains(City));
            }

            if (Rate != null)
            {

                doctors = doctors.Where(e => ((e.Rate <= Rate) && (e.Rate >= Rate)));
            }
            if (ConsultationFees != null)
            {
                if(ConsultationFees !=401)
                {
                    doctors = doctors.Where(e => ((e.ConsultationFees <= ConsultationFees) && (e.ConsultationFees >= ConsultationFees)));

                }
                else
                {
                    doctors = doctors.Where(e => ( (e.ConsultationFees >= ConsultationFees)));

                }
            }





            TempData["DoctorIndexTotalCount"] = doctors.Count();
            if (PageNumber < 1) PageNumber = 1;
            doctors = doctors.Skip((PageNumber - 1) * 5).Take(5);

            List <DoctorVM> doctorVMs = new List<DoctorVM>();

            foreach (var doctor in doctors)
            {
                var entity = _mapper.Map<DoctorVM>(doctor);
                //entity.Schedules = (List<Schedule>)_unitOfWork.ScheduleRepository.Retrive(filter: e => e.DoctorId == entity.Id);
                //entity.ClinicReceptionists = (List<ClinicReceptionist>)_unitOfWork.ClinicReceptionistRepository.Retrive(filter: e => e.DoctorId == entity.Id);
                //entity.qualifications = (List<Qualification>)_unitOfWork.QualificationRepository.Retrive(filter: e => e.DoctorId == entity.Id);
                //entity.QuestionAndAnswers = (List<QuestionAndAnswer>)_unitOfWork.QuestionAndAnswerRepository.Retrive(filter: e => e.DoctorId == entity.Id);
                //entity.RequestClinicReceptionists = (List<RequestClinicReceptionist>)_unitOfWork.RequestClinicReceptionistRepository.Retrive(filter: e => e.DoctorId == entity.Id);
                doctorVMs.Add(entity);
            }


            return View(doctorVMs);
        }
        [Route("Details")]
        public IActionResult Details(int DoctorId)
        {
            var doctor = this._unitOfWork.DoctorRepository.RetriveItem( filter: e=>e.Id == DoctorId, includeProps: [e =>e.Specialization]) as Doctor;
            if (doctor != null)
            {

                var doctorDetailsVM = this._mapper.Map<DoctorDetailsVM>(doctor);
                //doctorDetailsVM.Schedules = this._unitOfWork.ScheduleRepository.Retrive(filter: e => e.DoctorId == doctorDetailsVM.Id).ToList();
                //doctorDetailsVM.ClinicReceptionists = this._unitOfWork.ClinicReceptionistRepository.Retrive(filter: e => e.DoctorId == doctorDetailsVM.Id).ToList();
                //doctorDetailsVM.qualifications = this._unitOfWork.QualificationRepository.Retrive(filter: e => e.DoctorId == doctorDetailsVM.Id).ToList();
                //doctorDetailsVM.QuestionAndAnswers = this._unitOfWork.QuestionAndAnswerRepository.Retrive(filter: e => e.DoctorId == doctorDetailsVM.Id).ToList();
                //doctorDetailsVM.RequestClinicReceptionists = this._unitOfWork.RequestClinicReceptionistRepository.Retrive(filter: e => e.DoctorId == doctorDetailsVM.Id).ToList();

                return View(doctorDetailsVM);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("CreateEdit")]
        //upins
        public IActionResult CreateEdit(int DoctorId = 0)
        {
            var doctor = _unitOfWork.DoctorRepository.RetriveItem(filter : e=>e.Id == DoctorId);
            DoctorCreateEditVM doctorCreateEditVM = new DoctorCreateEditVM();
            
            if (doctor != null)
            {
                doctorCreateEditVM = _mapper.Map<DoctorCreateEditVM>(doctor);
                doctorCreateEditVM.CrudDoctorOption = CrudOption.Editing;
            }
            doctorCreateEditVM.Specializations = this._unitOfWork.SpecializationRepository.Retrive().ToList();
            doctorCreateEditVM.ApplicationUsers = this._userManager.Users.ToList();
            return View(doctorCreateEditVM);
        }
        [HttpPost]
        [Route("CreateEdit")]

        public IActionResult CreateEdit(DoctorCreateEditVM doctorCreateVM)
        {
            if (doctorCreateVM.ProfilePictureFile != null && doctorCreateVM.ProfilePictureFile.Length > 0)
            {
                doctorCreateVM.ProfilePicture = FileOperation.UploadFile(doctorCreateVM.ProfilePictureFile, "Images\\DoctorsPictures");
            }
           
            if (doctorCreateVM.PersonalNationalIDCardFile != null && doctorCreateVM.PersonalNationalIDCardFile.Length > 0)
            {
                doctorCreateVM.PersonalNationalIDCard = FileOperation.UploadFile(doctorCreateVM.PersonalNationalIDCardFile, "PersonalNationalIDCard");

            }
            
            if (doctorCreateVM.MedicalDegreeFile != null && doctorCreateVM.MedicalDegreeFile.Length > 0)
            {
                doctorCreateVM.MedicalDegree = FileOperation.UploadFile(doctorCreateVM.MedicalDegreeFile, "MedicalDegree");

            }
            
            if (doctorCreateVM.MedicalLicenseFile != null && doctorCreateVM.MedicalLicenseFile.Length > 0)
            {
                doctorCreateVM.MedicalLicense = FileOperation.UploadFile(doctorCreateVM.MedicalLicenseFile, "MedicalLicense");

            }
            
            if (doctorCreateVM.MedicalRegistrationFile != null && doctorCreateVM.MedicalRegistrationFile.Length > 0)
            {
                doctorCreateVM.MedicalRegistration = FileOperation.UploadFile(doctorCreateVM.MedicalRegistrationFile, "MedicalRegistration");

            }
            
            if (doctorCreateVM.MedicalIdentificationCardFile != null && doctorCreateVM.MedicalIdentificationCardFile.Length > 0)
            {
                doctorCreateVM.MedicalIdentificationCard = FileOperation.UploadFile(doctorCreateVM.MedicalIdentificationCardFile, "MedicalIdentificationCard");

            }
            
            if (doctorCreateVM.CrudDoctorOption == CrudOption.Creating)
            {
                return RedirectToAction(actionName:nameof(Create),controllerName :nameof(Doctor),routeValues: doctorCreateVM);
            }
            
            return RedirectToAction(actionName: nameof(Edit), controllerName: nameof(Doctor), routeValues: doctorCreateVM);
            

        }
        [Route("Create")]
        public IActionResult Create(DoctorCreateEditVM doctorCreateVM)
        {
            #region ModelState Remove
            ModelState.Remove("ProfilePicture");
            ModelState.Remove("ProfilePictureFile");
            ModelState.Remove("PersonalNationalIDCard");
            ModelState.Remove("MedicalDegree");
            ModelState.Remove("MedicalLicense");
            ModelState.Remove("MedicalRegistration");
            ModelState.Remove("MedicalIdentificationCard");
            ModelState.Remove("PersonalNationalIDCardFile");
            ModelState.Remove("MedicalDegreeFile");
            ModelState.Remove("MedicalLicenseFile");
            ModelState.Remove("MedicalRegistrationFile");
            ModelState.Remove("MedicalIdentificationCardFile");
            ModelState.Remove("CrudDoctorOption");
            ModelState.Remove("Specializations");
            ModelState.Remove("ApplicationUsers");
            ModelState.Remove("Specialization");

            #endregion
            if (ModelState.IsValid)
            {
                

                var doctor = _mapper.Map<Doctor>(doctorCreateVM);

                _unitOfWork.DoctorRepository.Create(doctor);
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
                        _userManager.AddToRoleAsync(user,Role.DoctorRole).GetAwaiter().GetResult();
                    }

                    createDoctorScheduleVM.DoctorId = savedDoctor.Id;
                    TempData["DoctorId"] = savedDoctor.Id;

                }
                return RedirectToAction("CreateDoctorSchedule" ,controllerName: "Schedule" , createDoctorScheduleVM);

            }
            return RedirectToAction("CreateEdit", routeValues: doctorCreateVM);
        }
        [Route("Edit")]
        public IActionResult Edit(DoctorCreateEditVM doctorCreateVM)
        {
            #region ModelState Remove
            ModelState.Remove("ProfilePicture");
            ModelState.Remove("ProfilePictureFile");
            ModelState.Remove("PersonalNationalIDCard");
            ModelState.Remove("MedicalDegree");
            ModelState.Remove("MedicalLicense");
            ModelState.Remove("MedicalRegistration");
            ModelState.Remove("MedicalIdentificationCard");
            ModelState.Remove("PersonalNationalIDCardFile");
            ModelState.Remove("MedicalDegreeFile");
            ModelState.Remove("MedicalLicenseFile");
            ModelState.Remove("MedicalRegistrationFile");
            ModelState.Remove("MedicalIdentificationCardFile");
            ModelState.Remove("CrudDoctorOption");
            ModelState.Remove("Specializations");
            ModelState.Remove("ApplicationUsers");
            ModelState.Remove("Specialization");

            #endregion
            if (ModelState.IsValid)
            {
                var oldDoctor = _unitOfWork.DoctorRepository.RetriveItem(filter : e=>e.Id ==  doctorCreateVM.Id,trancked:false);
                if(oldDoctor !=null)
                {
                    if(doctorCreateVM.ProfilePicture == "doctorDefault.webp")
                    {
                        doctorCreateVM.ProfilePicture = oldDoctor.ProfilePicture;
                    }
                    else
                    {
                        if(oldDoctor.ProfilePicture != "doctorDefault.webp")
                        {
                            FileOperation.DeleteFile(oldDoctor.ProfilePicture, "Images\\DoctorsPictures");

                        }
                    }

                    if (doctorCreateVM.PersonalNationalIDCard == "PersonalNationalIDCardDefault.jpg")
                    {
                        
                        doctorCreateVM.PersonalNationalIDCard = oldDoctor.PersonalNationalIDCard;
                    }
                    else
                    {
                        if(oldDoctor.PersonalNationalIDCard  != "PersonalNationalIDCardDefault.jpg")
                        {
                            FileOperation.DeleteFile(oldDoctor.PersonalNationalIDCard, "PersonalNationalIDCard");
                        }

                    }

                    if (doctorCreateVM.MedicalDegree == "MedicalDegree.jpg")
                    {

                        doctorCreateVM.MedicalDegree = oldDoctor.MedicalDegree;
                    }
                    else
                    {
                        if(oldDoctor.MedicalDegree != "MedicalDegree.jpg")
                        {
                            FileOperation.DeleteFile(oldDoctor.MedicalDegree, "MedicalDegree");

                        }

                    }

                    if (doctorCreateVM.MedicalIdentificationCard == "MedicalIdentificationCard.jpg")
                    {

                        doctorCreateVM.MedicalIdentificationCard = oldDoctor.MedicalIdentificationCard;
                    }
                    else
                    {
                        if (oldDoctor.MedicalIdentificationCard == "MedicalIdentificationCard.jpg")
                        {
                            FileOperation.DeleteFile(oldDoctor.MedicalIdentificationCard, "MedicalIdentificationCard");

                        }

                    }

                    if (doctorCreateVM.MedicalLicense == "MedicalLicense.webp")
                    {

                        doctorCreateVM.MedicalLicense = oldDoctor.MedicalLicense;
                    }
                    else
                    {
                        if(oldDoctor.MedicalLicense != "MedicalLicense.webp")
                        {
                            FileOperation.DeleteFile(oldDoctor.MedicalLicense, "MedicalLicense");

                        }

                    }

                    if (doctorCreateVM.MedicalRegistration == "MedicalRegistration.jpg")
                    {

                        doctorCreateVM.MedicalRegistration = oldDoctor.MedicalRegistration;
                    }
                    else
                    {
                        if(oldDoctor.MedicalRegistration != "MedicalRegistration.jpg")
                        {
                            FileOperation.DeleteFile(oldDoctor.MedicalRegistration, "MedicalRegistration");

                        }

                    }

                    var doctor = this._mapper.Map<Doctor>(doctorCreateVM);
                    _unitOfWork.DoctorRepository.Update(doctor);
                    _unitOfWork.Commit();
                }

                return RedirectToAction(nameof(Index));

            }
            return NotFound();
        }

        [Route("Delete")]
        public IActionResult Delete(int DoctorId)
        {
            var doctor = _unitOfWork.DoctorRepository.RetriveItem(filter: e => e.Id == DoctorId, trancked: false);

            if (doctor != null)
            {
                if (doctor.ProfilePicture != "doctorDefault.webp")
                {
                    FileOperation.DeleteFile(doctor.ProfilePicture, "Images\\DoctorsPictures");
                }

                if(doctor.PersonalNationalIDCard != "PersonalNationalIDCardDedault.jpg")
                {
                    FileOperation.DeleteFile(doctor.PersonalNationalIDCard, "PersonalNationalIDCard");

                }

                if (doctor.MedicalDegree != "MedicalDegree.jpg")
                {
                    FileOperation.DeleteFile(doctor.MedicalDegree, "MedicalDegree");

                }

                if (doctor.MedicalIdentificationCard != "MedicalIdentificationCard.jpg")
                {
                    FileOperation.DeleteFile(doctor.MedicalIdentificationCard, "MedicalIdentificationCard");

                }

                if (doctor.MedicalLicense != "MedicalLicense.webp")
                {
                    FileOperation.DeleteFile(doctor.MedicalLicense, "MedicalLicense");

                }

                if (doctor.MedicalRegistration != "MedicalRegistration.jpg")
                {
                    FileOperation.DeleteFile(doctor.MedicalRegistration, "MedicalRegistration");

                }



                // Change Role to Customer
                var user = _userManager.FindByIdAsync(doctor.ApplicationUserId).GetAwaiter().GetResult();
                if (user != null)
                {
                    _userManager.RemoveFromRolesAsync(user, _userManager.GetRolesAsync(user).GetAwaiter().GetResult()).GetAwaiter().GetResult();
                    _userManager.AddToRoleAsync(user, Role.DoctorRole).GetAwaiter().GetResult();
                }


                this._unitOfWork.DoctorRepository.Delete(doctor);
                this._unitOfWork.Commit();
            }

            return RedirectToAction(nameof(Index));  
        }

    }
}
