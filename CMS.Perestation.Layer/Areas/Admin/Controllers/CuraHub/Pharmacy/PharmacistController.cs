using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub.PharmacySection;
using CMS.Models.CuraHub.PharmacySection.PharmacySectionVM;
using CMS.Models.CuraHub.IdentitySection;
using Microsoft.AspNetCore.Identity;

namespace CMS.Perestation.Layer.Areas.Admin.Controllers.CuraHub.Pharmacy
{
    [Area(nameof(Admin))]
    [Route("Admin/CuraHub/Pharmacy/Pharmacist")]
    public class PharmacistController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private const int PageSize = 8;

        public PharmacistController(IUnitOfWork unitOfWork, IMapper mapper,  UserManager<ApplicationUser> userManager )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        private string HandleImageUpload(IFormFile file, string oldImagePath = "", string folder = "")
        {
            if (file == null || file.Length == 0) return oldImagePath;

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Images", "Pharmacists", folder, fileName);

            using (var stream = System.IO.File.Create(filePath))
            {
                file.CopyTo(stream);
            }

            if (!string.IsNullOrEmpty(oldImagePath))
            {
                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Images", "Pharmacists", folder, oldImagePath);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            return fileName;
        }

        private void SetPaginationData(int pageNumber, int totalItems)
        {
            ViewBag.currentPage = pageNumber;
            ViewBag.lastPage = (int)Math.Ceiling((double)totalItems / PageSize) - 1;
        }

        [Route("Index")]
        public IActionResult Index(int pageNumber = 0)
        {
            var pharmacists = _unitOfWork.PharmacistRepository
                .Retrive()
                .Skip(pageNumber * PageSize)
                .Take(PageSize)
                .ToList();

            int totalItems = _unitOfWork.PharmacistRepository.Retrive().Count();
            SetPaginationData(pageNumber, totalItems);

            var pharmacistsVM = _mapper.Map<List<PharmacistVM>>(pharmacists);
            return View(pharmacistsVM);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PharmacistVM pharmacistVM)
        {
            ModelState.Remove("ProfilePicture");
            ModelState.Remove("PersonalNationalIDCard");
            ModelState.Remove("ApplicationUserId");
            ModelState.Remove("MedicalDegree");

            if (ModelState.IsValid)
            {
                pharmacistVM.ProfilePicture = HandleImageUpload(pharmacistVM.FileProfile, folder: "Profiles");
                pharmacistVM.PersonalNationalIDCard = HandleImageUpload(pharmacistVM.FileNationalIDCard, folder: "IDCards");
                pharmacistVM.MedicalDegree = HandleImageUpload(pharmacistVM.FileMedicalDegree, folder: "MedicalDegrees");

                var random = new Random();
                string uniqueUserName = pharmacistVM.FirstName + random.Next(100, 999);

                // Create a new ApplicationUser
                var user = new ApplicationUser
                {
                    UserName = uniqueUserName,
                    Email = pharmacistVM.Email,
                    PhoneNumber = pharmacistVM.Phone,
                    FirstName = pharmacistVM.FirstName,
                    LastName = pharmacistVM.LastName,
                    ProfilePicture = pharmacistVM.ProfilePicture,
                };

                var result = await _userManager.CreateAsync(user, "Password123@"); // Use a strong password

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(pharmacistVM);
                }
                pharmacistVM.ApplicationUserId = user.Id; // Assign the generated User ID

                var pharmacist = _mapper.Map<Pharmacist>(pharmacistVM);
                _unitOfWork.PharmacistRepository.Create(pharmacist);
                _unitOfWork.Commit();

                int totalItems = _unitOfWork.PharmacistRepository.Retrive().Count();
                SetPaginationData(0, totalItems);

                return RedirectToAction(nameof(Index), new { pageNumber = ViewBag.lastPage });
            }
            return View(pharmacistVM);
        }

        [HttpGet]
        [Route("Edit")]
        public IActionResult Edit(int id, int pageNumber)
        {
            var pharmacist = _unitOfWork.PharmacistRepository.RetriveItem(p => p.Id == id);
            if (pharmacist == null) return NotFound();

            var pharmacistVM = _mapper.Map<PharmacistVM>(pharmacist);
            ViewBag.currentPage = pageNumber;

            return View(pharmacistVM);
        }

        [HttpPost]
        [Route("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PharmacistVM pharmacistVM, int pageNumber = 0)
        {
            ModelState.Remove("FileProfile");
            ModelState.Remove("FileNationalIDCard");
            ModelState.Remove("FileMedicalDegree");
            ModelState.Remove("PersonalNationalIDCard");
            ModelState.Remove("PersonalNationalIDNumber");
            ModelState.Remove("Email");
            ModelState.Remove("ApplicationUserId");

            var oldPharmacist = _unitOfWork.PharmacistRepository.RetriveItem(e => e.Id == pharmacistVM.Id, trancked: false);
            if (oldPharmacist == null) return NotFound();

            var user = await _userManager.FindByIdAsync(oldPharmacist.ApplicationUserId);
            if (user == null)
            {
                ModelState.AddModelError("", "Associated user not found.");
                return View(pharmacistVM);
            }
            
            if (ModelState.IsValid)
            {
                pharmacistVM.ProfilePicture = HandleImageUpload(pharmacistVM.FileProfile, pharmacistVM.ProfilePicture, "Profiles");
                pharmacistVM.PersonalNationalIDCard = HandleImageUpload(pharmacistVM.FileNationalIDCard, pharmacistVM.PersonalNationalIDCard, "IDCards");
                pharmacistVM.MedicalDegree = HandleImageUpload(pharmacistVM.FileMedicalDegree, pharmacistVM.MedicalDegree, "MedicalDegrees");

                user.PhoneNumber = pharmacistVM.Phone;
                user.ProfilePicture = pharmacistVM.ProfilePicture;
                user.FirstName = pharmacistVM.FirstName;
                user.LastName = pharmacistVM.LastName;

                var updateUserResult = await _userManager.UpdateAsync(user);
                if (!updateUserResult.Succeeded)
                {
                    foreach (var error in updateUserResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(pharmacistVM);
                }

                pharmacistVM.ApplicationUserId = oldPharmacist.ApplicationUserId;
                pharmacistVM.Email = oldPharmacist.Email;
                pharmacistVM.PersonalNationalIDNumber = oldPharmacist.PersonalNationalIDNumber;
                pharmacistVM.PersonalNationalIDCard = oldPharmacist.PersonalNationalIDCard;

                var pharmacist = _mapper.Map<Pharmacist>(pharmacistVM);
                _unitOfWork.PharmacistRepository.Update(pharmacist);
                _unitOfWork.Commit();

                return RedirectToAction(nameof(Index), new { pageNumber });
            }

            ViewBag.currentPage = pageNumber;
            return View(pharmacistVM);
        }

        [HttpGet]
        [Route("Details")]
        public IActionResult Details(int id, int pageNumber = 0)
        {
            var pharmacist = _unitOfWork.PharmacistRepository.RetriveItem(p => p.Id == id);
            if (pharmacist == null) return NotFound();

            ViewBag.currentPage = pageNumber;
            var pharmacistVM = _mapper.Map<PharmacistVM>(pharmacist);
            return View(pharmacistVM);
        }

        [HttpGet]
        [Route("Delete")]
        public IActionResult Delete(int id, int pageNumber = 0)
        {
            var pharmacist = _unitOfWork.PharmacistRepository.RetriveItem(p => p.Id == id);
            if (pharmacist == null) return NotFound();

            if (!string.IsNullOrEmpty(pharmacist.ProfilePicture))
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Images", "Pharmacists", "Profiles", pharmacist.ProfilePicture);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            if (!string.IsNullOrEmpty(pharmacist.PersonalNationalIDCard))
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Images", "Pharmacists", "IDCards", pharmacist.PersonalNationalIDCard);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            if (!string.IsNullOrEmpty(pharmacist.PersonalNationalIDCard))
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Images", "Pharmacists", "MedicalDegrees", pharmacist.MedicalDegree);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            _unitOfWork.PharmacistRepository.Delete(pharmacist);
            _unitOfWork.Commit();

            return RedirectToAction(nameof(Index), new { pageNumber });
        }
    }
}
