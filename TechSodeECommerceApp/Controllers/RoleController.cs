using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechSodeECommerceApp.Models;
using TechSodeECommerceApp.ViewModels;

namespace TechSodeECommerceApp.Controllers
{
    public class RoleController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Role/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var roleExists = await _roleManager.RoleExistsAsync(model.RoleName);
                if (!roleExists)
                {
                    var role = new IdentityRole(model.RoleName);
                    var result = await _roleManager.CreateAsync(role);

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index)); // Redirect to role listing page or dashboard
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Role already exists.");
                }
            }

            return View(model);
        }

        // Add an Index action to list all roles
        public async Task<IActionResult> Index()
        {
            var roles = _roleManager.Roles;
            return View(await roles.ToListAsync());
        }


        [HttpGet]
        public async Task<IActionResult> RoleMapping()
        {
            var users = await _userManager.Users.ToListAsync();
            var roles = await _roleManager.Roles.ToListAsync();

            var model = new RoleMappingViewModel
            {
                Users = users.Select(u => new SelectListItem { Value = u.Id, Text = u.Email }).ToList(),
                Roles = roles.Select(r => new SelectListItem { Value = r.Id, Text = r.Name }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RoleMapping(RoleMappingViewModel model)
        {
            if (!string.IsNullOrEmpty(model.SelectedUserId) && !string.IsNullOrEmpty(model.SelectedRoleId))
            {
                var user = await _userManager.FindByIdAsync(model.SelectedUserId);
                var role = await _roleManager.FindByIdAsync(model.SelectedRoleId);

                if (user != null && role != null)
                {
                    var result = await _userManager.AddToRoleAsync(user, role.Name);

                    if (result.Succeeded)
                    {
                        ViewBag.Message = "Role assigned successfully.";
                    }
                    else
                    {
                        ViewBag.Error = "Failed to assign role.";
                    }
                }
            }

            // Re-populate the Users and Roles dropdown lists after form submission
            model.Users = _userManager.Users.Select(u => new SelectListItem { Value = u.Id, Text = u.Email }).ToList();
            model.Roles = _roleManager.Roles.Select(r => new SelectListItem { Value = r.Id, Text = r.Name }).ToList();

            return View(model);
        }

    }
}
