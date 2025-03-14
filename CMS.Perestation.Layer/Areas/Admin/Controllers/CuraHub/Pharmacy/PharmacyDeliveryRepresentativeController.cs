using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub.PharmacySection.PharmacySectionVM;
using CMS.Models.CuraHub.PharmacySection;
using Microsoft.AspNetCore.Identity;
using CMS.Models.CuraHub.IdentitySection;


namespace CMS.Perestation.Layer.Areas.Admin.Controllers.CuraHub.Pharmacy
{
    [Area(nameof(Admin))]
    [Route("Admin/CuraHub/Pharmacy/pharmacyDeliveryRepresentative")]
    public class PharmacyDeliveryRepresentativeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private const int PageSize = 8;


        public PharmacyDeliveryRepresentativeController(IUnitOfWork unitOfWork, IMapper mapper,  UserManager<ApplicationUser> userManager ) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        private string HandleImageUpload(IFormFile file, string oldImagePath = "", string folder = "")
        {
            if (file == null || file.Length == 0) return oldImagePath;

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Images", "PharmacyDelivery", folder, fileName);

            using (var stream = System.IO.File.Create(filePath))
            {
                file.CopyTo(stream);
            }

            if (!string.IsNullOrEmpty(oldImagePath))
            {
                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Images", "PharmacyDelivery", folder, oldImagePath);
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
        public IActionResult Index(int pageNumber)
        {
            var Delivery= _unitOfWork.PharmacyDeliveryRepresentativeRepository
            .Retrive()
            .Skip(pageNumber * PageSize)
            .Take(PageSize)
            .ToList();

            int totalItems = _unitOfWork.PharmacyDeliveryRepresentativeRepository.Retrive().Count();
            SetPaginationData(pageNumber, totalItems);

            var DeliveryVM = _mapper.Map<List<PharmacyDeliveryRepresentativeVM>>(Delivery);
            return View(DeliveryVM);
        }
        [HttpGet]
        [Route("Create")]
        public IActionResult Create(int pageNumber = 0)
        {
            ViewBag.currentPage = pageNumber;
            return View();
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PharmacyDeliveryRepresentativeVM PharmacyDeliveryVM)
        {
            ModelState.Remove("ProfilePicture");
            ModelState.Remove("PersonalNationalIDCard");
            ModelState.Remove("ApplicationUserId");

            if (ModelState.IsValid)
            {
                PharmacyDeliveryVM.ProfilePicture = HandleImageUpload(PharmacyDeliveryVM.FileProfile, folder: "Profiles");
                PharmacyDeliveryVM.PersonalNationalIDCard = HandleImageUpload(PharmacyDeliveryVM.FileNationalIDCard, folder: "IDCards");


                var random = new Random();
                string uniqueUserName = PharmacyDeliveryVM.FirstName + random.Next(100, 999);

                // Create a new ApplicationUser
                var user = new ApplicationUser
                {
                    UserName = uniqueUserName,
                    Email = PharmacyDeliveryVM.Email,
                    PhoneNumber = PharmacyDeliveryVM.Phone,
                    FirstName = PharmacyDeliveryVM.FirstName,
                    LastName = PharmacyDeliveryVM.LastName,
                    ProfilePicture = PharmacyDeliveryVM.ProfilePicture,
                };

                var result = await _userManager.CreateAsync(user, "Password123@"); // Use a strong password

                if (!result.Succeeded)
                {
                    // Debugging output
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(PharmacyDeliveryVM);
                }

                PharmacyDeliveryVM.ApplicationUserId = user.Id; // Assign the generated User ID

                var PharmacyDelivery = _mapper.Map<PharmacyDeliveryRepresentative>(PharmacyDeliveryVM);
                _unitOfWork.PharmacyDeliveryRepresentativeRepository.Create(PharmacyDelivery);
                _unitOfWork.Commit();

                int totalItems = _unitOfWork.PharmacyDeliveryRepresentativeRepository.Retrive().Count();
                SetPaginationData(0, totalItems);
                
                return RedirectToAction(nameof(Index), new { pageNumber = ViewBag.lastPage });
            }

            return View(PharmacyDeliveryVM);
        }




        [HttpGet]
        [Route("Edit")]
        public IActionResult Edit(int id, int pageNumber = 0)
        {
            var Delivery= _unitOfWork.PharmacyDeliveryRepresentativeRepository.RetriveItem(p => p.Id == id);
            if (Delivery == null) return NotFound();

            var DeliveryVM = _mapper.Map<PharmacyDeliveryRepresentativeVM>(Delivery);
            ViewBag.currentPage = pageNumber;

            return View(DeliveryVM);
        }

        [HttpPost]
        [Route("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PharmacyDeliveryRepresentativeVM PharmacyDeliveryVM, int pageNumber = 0)
        {
            ModelState.Remove("ProfilePicture");
            ModelState.Remove("FileProfile");
            ModelState.Remove("PersonalNationalIDCard");
            ModelState.Remove("PersonalNationalIDNumber");
            ModelState.Remove("FileNationalIDCard");
            ModelState.Remove("Email");
            ModelState.Remove("ApplicationUserId");
            
            var oldDelivery = _unitOfWork.PharmacyDeliveryRepresentativeRepository.RetriveItem(e => e.Id == PharmacyDeliveryVM.Id, trancked: false);
            if (oldDelivery == null) return NotFound();

            var user = await _userManager.FindByIdAsync(oldDelivery.ApplicationUserId);
            if (user == null)
            {
                ModelState.AddModelError("", "Associated user not found.");
                return View(PharmacyDeliveryVM);
            }

            if (ModelState.IsValid)
            {
                PharmacyDeliveryVM.ProfilePicture = HandleImageUpload(PharmacyDeliveryVM.FileProfile, oldDelivery.ProfilePicture, "Profiles");
                PharmacyDeliveryVM.PersonalNationalIDCard = HandleImageUpload(PharmacyDeliveryVM.FileNationalIDCard, oldDelivery.PersonalNationalIDCard, "IDCards");

                user.PhoneNumber = PharmacyDeliveryVM.Phone;
                user.ProfilePicture = PharmacyDeliveryVM.ProfilePicture;
                user.FirstName = PharmacyDeliveryVM.FirstName;
                user.LastName = PharmacyDeliveryVM.LastName;

                var updateUserResult = await _userManager.UpdateAsync(user);
                if (!updateUserResult.Succeeded)
                {
                    foreach (var error in updateUserResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(PharmacyDeliveryVM);
                }

                PharmacyDeliveryVM.ApplicationUserId = oldDelivery.ApplicationUserId;
                PharmacyDeliveryVM.Email = oldDelivery.Email;
                PharmacyDeliveryVM.PersonalNationalIDNumber = oldDelivery.PersonalNationalIDNumber;
                PharmacyDeliveryVM.PersonalNationalIDCard = oldDelivery.PersonalNationalIDCard;

                var pharmacyDeliveryRepresentative = _mapper.Map<PharmacyDeliveryRepresentative>(PharmacyDeliveryVM);
                _unitOfWork.PharmacyDeliveryRepresentativeRepository.Update(pharmacyDeliveryRepresentative);
                _unitOfWork.Commit();

                return RedirectToAction(nameof(Index), new { pageNumber });

            }
            ViewBag.currentPage = pageNumber;
            return View(PharmacyDeliveryVM);
        }
        [HttpGet]
        [Route("Details")]
        public IActionResult Details(int id, int pageNumber = 0)
        {
            var Delivery= _unitOfWork.PharmacyDeliveryRepresentativeRepository.RetriveItem(d => d.Id == id, [d => d.PharmacyOrders]);
            if (Delivery== null) return NotFound();

            ViewBag.currentPage = pageNumber;
            var DeliveryVM = _mapper.Map<PharmacyDeliveryRepresentativeVM>(Delivery);
            
            return View(DeliveryVM);
        }

        [HttpGet]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id, int pageNumber = 0)
        {
            var delivery = _unitOfWork.PharmacyDeliveryRepresentativeRepository.RetriveItem(p => p.Id == id);
            if (delivery== null) return NotFound();

            var user = await _userManager.FindByIdAsync(delivery.ApplicationUserId);

            if (delivery == null) return NotFound();
            // Delete Photo
            var imagePathProfile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Images", "PharmacyDelivery", "Profiles", delivery.ProfilePicture);
            var imagePathId = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Images", "PharmacyDelivery", "IDCards", delivery.PersonalNationalIDCard);
            if (System.IO.File.Exists(imagePathProfile))
            {
                System.IO.File.Delete(imagePathProfile);
            }
            if (System.IO.File.Exists(imagePathId))
            {
                System.IO.File.Delete(imagePathId);
            }
            // Delete User
            if (user != null)
            {
                var deleteUserResult = await _userManager.DeleteAsync(user);
                if (!deleteUserResult.Succeeded)
                {
                    foreach (var error in deleteUserResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return RedirectToAction(nameof(Index), new { pageNumber });
                }
            }

            _unitOfWork.PharmacyDeliveryRepresentativeRepository.Delete(delivery);
            _unitOfWork.Commit();

            return RedirectToAction(nameof(Index), new { pageNumber });
        }
        [HttpGet]
        [Route("Search")]
        public IActionResult Search(string searchText, int pageNumber = 0)
        {
            var deliver = _unitOfWork.PharmacyDeliveryRepresentativeRepository
                .Retrive(e => e.FirstName.ToLower().Contains(searchText.ToLower()) || e.LastName.ToLower().Contains(searchText.ToLower()) || e.Email.ToLower().Contains(searchText) || e.Phone.ToLower().Contains(searchText))
                .Skip(pageNumber * PageSize)
                .Take(PageSize)
                .ToList();

            var deliverVM = _mapper.Map<List<PharmacyDeliveryRepresentativeVM>>(deliver);
            return PartialView("_Search", deliverVM);
        }
    }
}