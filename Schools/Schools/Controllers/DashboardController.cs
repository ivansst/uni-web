using Microsoft.AspNetCore.Mvc;

namespace Schools.Controllers
{
  public class DashboardController : Controller
    {
        [HttpGet]
        public IActionResult Admin()
        {
            return View(nameof(Admin));
        }

        [HttpGet]
        public IActionResult Principal()
        {
            return View(nameof(Principal));
        }

        [HttpGet]
        public IActionResult Teacher()
        {
            return View(nameof(Teacher));
        }

        [HttpGet]
        public IActionResult Student()
        {
            return View(nameof(Student));
        }

        [HttpGet]
        public IActionResult Parent()
        {
            return View(nameof(Parent));
        }

    }
}
