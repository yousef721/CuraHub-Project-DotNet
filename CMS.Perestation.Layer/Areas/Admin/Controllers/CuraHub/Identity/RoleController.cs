using CMS.Models.CuraHub.IdentitySection.IdentitySectionVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Perestation.Layer.Areas.Admin.Controllers.CuraHub.Identity
{
    [Area(nameof(Admin))]
    //[Authorize(Roles = ($"{Role.AdminRole}"))]
    [Route("Admin/CuraHub/Identity/Role")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this._roleManager = roleManager;
        }
        [Route("Index")]

        public IActionResult Index(string? query = null, int PageNumber = 1)
        {
            ApplicationRolesVM applicationRolesVM = new ApplicationRolesVM();
            var roles = _roleManager.Roles;

            if (query != null)
            {
                query = query.Trim();
                roles = roles.Where(e => e.Name.Contains(query));
            }
            applicationRolesVM.TotalRoleCount = (roles.Count() + 4) / 5;
            if (PageNumber < 1) PageNumber = 1;
            roles = roles.Skip((PageNumber - 1) * 5).Take(5);
            applicationRolesVM.CurrentPageIndex = PageNumber;
            applicationRolesVM.roles = roles.ToList();
            return View(applicationRolesVM);


        }
        [HttpGet]

        [Route("Create")]

        public IActionResult Create()
        {
            return View(new ApplicationRoleVM());
        }
        [HttpPost]
        [Route("Create")]

        public async Task<IActionResult> Create(ApplicationRoleVM applicationRoleVM)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new(roleName: applicationRoleVM.Name));

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(applicationRoleVM);
        }

    }
}
