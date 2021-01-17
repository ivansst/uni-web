using Microsoft.AspNetCore.Mvc;

namespace Schools.Controllers
{
  public class DashboardController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}
