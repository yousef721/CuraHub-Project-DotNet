using AutoMapper;
using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub.ClinicSection;
using CMS.Models.CuraHub.ClinicSection.ClinicSectionVM;
using CMS.Models.Enums;
using CMS.Utitlities.Helper;
using CMS.Utitlities.StaticData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Numerics;

namespace CMS.Perestation.Layer.Areas.Admin.Controllers.CuraHub.Clinic
{
    [Area(nameof(Admin))]
    [Authorize(Roles = ($"{Role.AdminRole}"))]
    [Route("Admin/CuraHub/Clinic/Qualification")]
    public class QualificationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public QualificationController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        [Route("Index")]
        public IActionResult Index(string? query=null , int PageNumber = 1 , int? doctorId=null )
        {
            var qualifications = this._unitOfWork.QualificationRepository.Retrive(includeProps: [e=>e.Doctor]);
            if (query != null)
            {
                query =query.Trim();
                qualifications = qualifications.Where(e=>e.Name.Contains(query));
            }
            if (doctorId != null)
            {
                qualifications = qualifications.Where(e =>e.Doctor.Id == doctorId);
            }
            if (PageNumber < 1) PageNumber = 1;
            QualificationsVM qualificationsVM = new QualificationsVM();
            qualifications = qualifications.Skip((PageNumber - 1) * 5).Take(5);
            qualificationsVM.CurrentPageNumber = PageNumber;
            qualificationsVM.TotalQualificationsCount = qualifications.Count();
            qualificationsVM.Qualifications = qualifications.ToList();
            qualificationsVM.Doctors = this._unitOfWork.DoctorRepository.Retrive().ToList();

            return View(qualificationsVM);
        }
        [HttpGet]
        [Route("CreateEdit")]
        public IActionResult CreateEdit(int QualificationId=0)
        {
            var qualification  = this._unitOfWork.QualificationRepository.RetriveItem(filter: e=>e.Id ==  QualificationId , trancked:false);
            QualificationCreateEditVM qualificationCreateEditVM = new QualificationCreateEditVM();
            if (qualification != null)
            {
                qualificationCreateEditVM = this._mapper.Map<QualificationCreateEditVM>(qualification);
                qualificationCreateEditVM.CrudQualificationOption = CrudOption.Editing;

            }
            qualificationCreateEditVM.Doctors = this._unitOfWork.DoctorRepository.Retrive().ToList();

            return View(qualificationCreateEditVM);
        }
        [HttpPost]
        [Route("CreateEdit")]
        public IActionResult CreateEdit(QualificationCreateEditVM qualificationCreateEditVM)
        {
            if (qualificationCreateEditVM.CertificationFile != null && qualificationCreateEditVM.CertificationFile.Length > 0)
            {
                qualificationCreateEditVM.Certification = FileOperation.UploadFile(qualificationCreateEditVM.CertificationFile, "Qualifications");
            }

            if (qualificationCreateEditVM.CrudQualificationOption == CrudOption.Creating)
            {
                return RedirectToAction(actionName: nameof(Create), controllerName: nameof(Qualification), routeValues: qualificationCreateEditVM);
            }
            return RedirectToAction(actionName: nameof(Edit), controllerName: nameof(Qualification), routeValues: qualificationCreateEditVM);
        }
        [Route("Create")]
        public IActionResult Create(QualificationCreateEditVM qualificationCreateEditVM)
        {
            ModelState.Remove("CertificationFile");
            ModelState.Remove("Certification");
            ModelState.Remove("Doctors");
            ModelState.Remove("CrudQualificationOption");
            ModelState.Remove("Doctor");

            if (ModelState.IsValid) 
            {
                var qualification = _mapper.Map<Qualification>(qualificationCreateEditVM);
                this._unitOfWork.QualificationRepository.Create(qualification);
                this._unitOfWork.Commit();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("CreateEdit", routeValues: qualificationCreateEditVM);
        }

        [Route("Edit")]
        public IActionResult Edit(QualificationCreateEditVM qualificationCreateEditVM)
        {
            ModelState.Remove("CertificationFile");
            ModelState.Remove("Certification");
            ModelState.Remove("Doctors");
            ModelState.Remove("CrudQualificationOption");
            ModelState.Remove("Doctor");

            if (ModelState.IsValid)
            {
                var oldQualification = this._unitOfWork.QualificationRepository.RetriveItem(filter: e => e.Id == qualificationCreateEditVM.Id,trancked: false);
                if(oldQualification != null)
                {
                    if (qualificationCreateEditVM.Certification == "certication.webp")
                    {
                        qualificationCreateEditVM.Certification = oldQualification.Certification;
                    }
                    else
                    {
                        if (qualificationCreateEditVM.Certification != "certication.webp")
                        {
                            FileOperation.DeleteFile(oldQualification.Certification, "Qualifications");
                        }

                       

                    }
                    var qualification = this._mapper.Map<Qualification>(qualificationCreateEditVM);
                    this._unitOfWork.QualificationRepository.Update(qualification);
                    this._unitOfWork.Commit();
                    return RedirectToAction(nameof(Index));
                }
            }
            return NotFound();

        }

        [Route("Delete")]
        public IActionResult Delete(int qualificationId)
        {
            var qualification = this._unitOfWork.QualificationRepository.RetriveItem(filter: e => e.Id == qualificationId, trancked: false);
            if(qualification!=null)
            {
                if (qualification.Certification != "certication.webp")
                {
                    FileOperation.DeleteFile(qualification.Certification, "Qualifications");
                }
                this._unitOfWork.QualificationRepository.Delete(qualification);
                this._unitOfWork.Commit();
                TempData["success"] = "Delete Qualification successfuly";

            }
            return RedirectToAction(nameof(Index));

        }
    }
}
