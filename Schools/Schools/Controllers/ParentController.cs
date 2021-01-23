using Microsoft.AspNetCore.Mvc;
using Schools.ViewModels;

namespace Schools.Controllers
{
  public class ParentController : Controller
  {


    [HttpGet]
    public IActionResult Edit(string userId)
    {
      return View();
    }

    [HttpPost]
    public IActionResult Edit(ParentEditViewModel model)
    {
      return View();
    }
  }
}
