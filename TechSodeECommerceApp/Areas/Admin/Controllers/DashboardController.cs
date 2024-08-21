using Microsoft.AspNetCore.Mvc;

namespace TechSodeECommerceApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddCategory()
        {

            return View();
        }

        public IActionResult Categories()
        {

            return View();
        }
    }
}
