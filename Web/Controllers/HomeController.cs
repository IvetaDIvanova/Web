using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using Web.Models;

namespace Web.Controllers
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
            var theme = Request.Cookies["UserPreferences"] ?? "Light";
            ViewBag.Theme = theme;
            return View();
        }

        [HttpPost]
        public IActionResult ChangeTheme(string theme)
        {
            Response.Cookies.Append("UserPreferences", theme, new CookieOptions
            {
                Expires = DateTime.UtcNow.AddYears(1),
                HttpOnly = true,
                IsEssential = true
            });
            return RedirectToAction("Index");
            
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

        //here we have a comment :D
        //lmao
    }
}
