using AutoMapper;
using CMS.Models.CuraHub.IdentitySection;
using CMS.Models.CuraHub.IdentitySection.IdentitySectionVM;
using CMS.Utitlities.StaticData;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Perestation.Layer.Areas.Admin.Controllers.CuraHub.Identity
{
    [Area(nameof(Admin))]
    //[Authorize(Roles = ($"{Role.AdminRole}"))]
    [Route("Admin/CuraHub/Identity/User")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;


        }
        [Route("Index")]

        public IActionResult Index(string? query = null, int PageNumber = 1)
        {
            var users = _userManager.Users;
            if (query != null)
            {
                query = query.Trim();
                users = users.Where(e => e.FirstName.Contains(query) || e.LastName.Contains(query)|| e.Email.Contains(query)|| e.UserName.Contains(query));
            }
            if (PageNumber < 1) PageNumber = 1;
            TempData["UserCount"] = users.Count();
            users = users.Skip((PageNumber - 1) * 5).Take(5);
            List<ApplicationUserVM> usersVM = new List<ApplicationUserVM>();
            foreach (var item in users)
            {
                var ItemRole = _userManager.GetRolesAsync(item).Result.FirstOrDefault();
                var userVM = _mapper.Map<ApplicationUserVM>(item);
                if(ItemRole !=null)
                {
                    userVM.Role = ItemRole;

                }
                usersVM.Add(userVM);
            }
            return View(usersVM);
        }
        [Route("Lock")]
        public async Task<IActionResult> Lock(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user != null)
            {
                user.LockoutEnabled = false;
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction("Index", "User", new { area = "Admin" });
        }
        [Route("UnLock")]
        public async Task<IActionResult> UnLock(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user != null)
            {
                user.LockoutEnabled = true;
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction("Index", "User", new { area = "Admin" });
        }
        [HttpGet]
        [Route("RoleManagement")]
        public async Task<IActionResult> RoleManagement(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user is not null)
            {
                var roles = _roleManager.Roles.ToList();
                UserRoleVM userRoleVM = new UserRoleVM();
                userRoleVM.Roles = roles;
                userRoleVM.User = user;
                return View(userRoleVM);
            }
            return View(user);
        }
        [HttpPost]
        [Route("RoleManagement")]
        public async Task<IActionResult> RoleManagement(string UserId, string RoleId)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(UserId);
                var role = await _roleManager.FindByIdAsync(RoleId);
                await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
                await _userManager.AddToRoleAsync(user, role.Name);

            }
            return RedirectToAction("Index");
        }
    }
}
