using AutoMapper;
using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub.ClinicSection;
using CMS.Models.CuraHub.ClinicSection.ClinicSectionVM;
using CMS.Utitlities.Helper;
using CMS.Utitlities.StaticData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CMS.Perestation.Layer.Areas.Admin.Controllers.CuraHub.Clinic
{
    [Area(nameof(Admin))]
    [Authorize(Roles = ($"{Role.AdminRole}"))]
    [Route("Admin/CuraHub/Clinic/Specialization")]
    public class SpecializationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public SpecializationController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        [Route("Index")]
        public IActionResult Index(string? query = null, int PageNumber = 1)
        {
            var specializations = _unitOfWork.SpecializationRepository.Retrive();
            if (query != null)
            {
                query = query.Trim();

                specializations = specializations.Where(e => e.Name.Contains(query));
            }
            if (PageNumber < 1) PageNumber = 1;
            TotalSpecializationsVM totalSpecializationsVM = new TotalSpecializationsVM();
            totalSpecializationsVM.TotalSpecializationCount = specializations.Count();
            totalSpecializationsVM.CurrentPage = PageNumber;
            specializations = specializations.Skip((PageNumber - 1) * 5).Take(5);
            totalSpecializationsVM.specializations = specializations.ToList();
            return View(totalSpecializationsVM);
        }
        [HttpGet]
        [Route("CreateEdit")]
        public IActionResult CreateEdit(int SpecializationId = 0)
        {
            var specialization = _unitOfWork.SpecializationRepository.RetriveItem(filter: e => e.Id == SpecializationId);
            var specializationVM = new SpecializationVM();
            if (specialization != null)
            {
                specializationVM = _mapper.Map<SpecializationVM>(specialization);
                specializationVM.specializationsOperation = SpecializationsOperation.Editing;
            }
            else
            {
                specializationVM.Id = 0;
                specializationVM.Name = string.Empty;
                specializationVM.Icon = "MedicalLogo.jpg";
                specializationVM.specializationsOperation = SpecializationsOperation.Creating;

            }

            return View(specializationVM);

                 
        }

        [HttpPost]
        [Route("CreateEdit")]
        public IActionResult CreateEdit(SpecializationVM specializationsVM , IFormFile file)
        {
            ModelState.Remove("Icon");
            ModelState.Remove("file");
            ModelState.Remove("specializationsOperation");

            if (ModelState.IsValid)
            {    var OldSpecialization = _unitOfWork.SpecializationRepository.RetriveItem(filter: e => e.Id == specializationsVM.Id , trancked:false);

                if(file !=null && file.Length >0)
                {
                    specializationsVM.Icon = FileOperation.UploadFile(file , "Images\\SpecializationLogo");

                    if (OldSpecialization !=null)
                    {
                        FileOperation.DeleteFile(OldSpecialization.Icon, "Images\\SpecializationLogo");
                    }
                }
                else
                {
                    if (OldSpecialization != null)
                    {
                        specializationsVM.Icon = OldSpecialization.Icon;
                    }
                    else
                    {
                        specializationsVM.Icon = "MedicalLogo.jpg";

                    }
                }
                var specialization = _mapper.Map<Specialization>(specializationsVM);
                if(specializationsVM.specializationsOperation == SpecializationsOperation.Creating)
                {
                    _unitOfWork.SpecializationRepository.Create(specialization);

                }
                else
                {
                    _unitOfWork.SpecializationRepository.Update(specialization);

                }
                _unitOfWork.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(specializationsVM);
        }

        [Route("Delete")]

        public IActionResult Delete(int SpecializationId)
        {
            var specialization = this._unitOfWork.SpecializationRepository.RetriveItem(filter: e => e.Id == SpecializationId);

            FileOperation.DeleteFile(specialization.Icon, "Images\\SpecializationLogo");



            if (specialization != null)
            {
                this._unitOfWork.SpecializationRepository.Delete(specialization);
                this._unitOfWork.Commit();
            }
            return RedirectToAction(nameof(Index));

        }
 
      
    }
}
