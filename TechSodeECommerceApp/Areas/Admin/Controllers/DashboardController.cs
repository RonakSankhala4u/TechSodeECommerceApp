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
        public IActionResult Products()
        {

            return View();
        }
        public IActionResult AddProduct()
        {
            return View();
        }
        public IActionResult BlogList()
        {
            return View();
        }
        public IActionResult BlogGrid()
        {
            return View();
        }
        public IActionResult AddBlog()
        {
            return View();
        }
        public IActionResult EditBlog()
        {
            return View();
        }public IActionResult BlogDetails()
        {
            return View();
        }
        public IActionResult Customers()
        {
            return View();
        }
        public IActionResult AddCustomer()
        {
            return View();
        }
        public IActionResult EditCustomer()
        {
            return View();
        }
        public IActionResult OrderList()
        {
            return View();
        }
        public IActionResult OrderDetails()
        {
            return View();
        }
        public IActionResult Reviews()
        {
            return View();
        }
        public IActionResult VendorList()
        {
            return View();
        }
        public IActionResult VendorGrid()
        {
            return View();
        }

        

    }
}
