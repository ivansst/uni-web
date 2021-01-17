using Microsoft.AspNetCore.Mvc;

namespace Schools.Controllers
{
  public class DashBoardController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}
