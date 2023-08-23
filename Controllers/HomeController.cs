using Microsoft.AspNetCore.Mvc;
using ProjectMVC_Ecommerce.Models;
using System.Diagnostics;

namespace ProjectMVC_Ecommerce.Controllers
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
        [Route("conatact.html", Name ="Contact")]
        public IActionResult Contact()
        {
            return View();
        }
        [Route("about.html", Name = "About")]

        public IActionResult About()
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
    }
}