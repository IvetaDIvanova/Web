using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class SessionController : Controller
    {
        public IActionResult Index()
        {
            var session = GetSession();
            ViewBag.Session = session;
            return View();
        }

        public IActionResult SetSession()
        {
            HttpContext.Session.SetString("username", "Ivan Ivanov");
            return Content("The session was created successfully!");
        }

        public IActionResult GetSession()
        { 
            var username = HttpContext.Session.GetString("username");
            if(string.IsNullOrEmpty(username))
            {
                return Content("There is no such saved session value.");
            }
            return Content($"Saved name in the session: {username}");
        }

        public IActionResult ClearSession()
        {
            HttpContext.Session.Clear();
            return Content("The session was cleared!");
        }

        public IActionResult ActiveSession()
        {
            var username = HttpContext.Session.GetString("username");
            if (string.IsNullOrEmpty(username))
            {
                return Content("There is no such active session!!!");
            }

            return Content($"The session is active as: {username}");
        }
    }
}
