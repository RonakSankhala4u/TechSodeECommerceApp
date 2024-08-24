using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechSodeECommerceApp.ViewModels;

namespace TechSodeECommerceApp.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
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
    }
}
