using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TechSodeECommerceApp.Models;

namespace TechSodeECommerceApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Error404()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult AccountAddress()
        {
            return View();
        }

        public IActionResult AccountNotification()
        {
            return View();
        }

        public IActionResult AccountOrders()
        {
            return View();
        }

        public IActionResult AccountPaymentMethod()
        {
            return View();
        }

        public IActionResult AccountSettings()
        {
            return View();
        }

        public IActionResult Blog()
        {
            return View();
        }

        public IActionResult BlogCategory()
        {
            return View();
        }

        public IActionResult BlogSingle()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }


        public IActionResult Index2()
        {
            return View();
        }

        public IActionResult Index3()
        {
            return View();
        }

        public IActionResult Index4()
        {
            return View();
        }

        public IActionResult Index5()
        {
            return View();
        }

        public IActionResult ShopCart()
        {
            return View();
        }

        public IActionResult ShopCheckout()
        {
            return View();
        }

        public IActionResult ShopFilter()
        {
            return View();
        }

        public IActionResult ShopFullWidth()
        {
            return View();
        }

        public IActionResult ShopGrid()
        {
            return View();
        }

        public IActionResult ShopGrid3Column()
        {
            return View();
        }

        public IActionResult ShopList()
        {
            return View();
        }

        public IActionResult ShopSingle()
        {
            return View();
        }

        public IActionResult ShopSingle2()
        {
            return View();
        }

        public IActionResult ShopWishlist()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult StoreGrid()
        {
            return View();
        }

        public IActionResult StoreList()
        {
            return View();
        }

        public IActionResult StoreSingle()
        {
            return View();
        }


    }
}
