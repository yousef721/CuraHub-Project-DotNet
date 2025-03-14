using AutoMapper;
using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.PatientVM;
using CMS.Models.CuraHub.IdentitySection;
using CMS.Utitlities.StaticData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Perestation.Layer.Areas.Admin.Controllers.CuraHub.Clinic
{
    [Area(nameof(Admin))]
    [Authorize(Roles = ($"{Role.AdminRole}"))]
    [Route("Admin/CuraHub/Clinic/Patient")]
    public class PatientController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;


        public PatientController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        [Route("Index")]
        public IActionResult Index(string? query = null, int PageNumber = 1, string? State = null, string? City = null)
        {
            var Pateints = this._unitOfWork.PatientRepository.Retrive();

            if (query != null)
            {
                query = query.Trim();

                Pateints = Pateints.Where(e => e.FirstName.Contains(query) || e.LastName.Contains(query) || e.PersonalNationalIDNumber.Contains(query));
            }
            if (State != null)
            {
                State = State.Trim();

                Pateints = Pateints.Where(e => e.State.Contains(State));
            }
            if (City != null)
            {
                City = City.Trim();

                Pateints = Pateints.Where(e => e.City.Contains(City));
            }

            Admin_PatientsVM admin_PatientsVM = new Admin_PatientsVM();

            admin_PatientsVM.Patients = Pateints.ToList();
            admin_PatientsVM.CurrentPageNumber = PageNumber;    
            admin_PatientsVM.TotalPatientCount = Pateints.Count();



            return View(admin_PatientsVM);
        }
        [Route("Details")]
        public IActionResult Details(int PatientId)
        {
            var pateint = _unitOfWork.PatientRepository.RetriveItem(filter :e =>e.Id == PatientId);
            if (pateint != null)
            {
                return View(pateint);
            }
            return RedirectToAction("Index");
        }

        [Route("Delete")]

        public IActionResult Delete(int PatientId)
        {
            var patient = _unitOfWork.PatientRepository.RetriveItem(filter: e => e.Id == PatientId);
            if (patient != null)
            {
                this._unitOfWork.PatientRepository.Delete(patient);
                this._unitOfWork.Commit();
            }
            return RedirectToAction("Index");
        }
    }

}
