using CMS.Perestation.Layer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CMS.Perestation.Layer.Areas.Customer.Controllers
{
    [Area(nameof(Customer))]

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

        public IActionResult MemberRequest()
        {
            return View();
        }        
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        // /Customer/Home/NotFoundPage?message=no
        public IActionResult NotFoundPage(string? Message)
        {
            return View(new { Message });
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
