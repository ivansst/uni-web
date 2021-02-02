using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schools.Data.Models;
using Schools.Services.Interfaces;
using System.Threading.Tasks;

namespace Schools.Controllers
{
  [Authorize]
  public class DashboardController : BaseController
  {

    private readonly ISchoolService schoolService;
    private readonly IUserService userService;

    public DashboardController(ISchoolService schoolService, IUserService userService)
    {
      this.userService = userService;
      this.schoolService = schoolService;
    }

    [HttpGet]
    public async Task<IActionResult> Administrator()
    {

      ViewData["schoolId"] = null;

      var model = await this.schoolService.GetAll();

      return View(nameof(Administrator), model);
    }

    [HttpGet]
    public async Task<IActionResult> Principal()
    {

      var schoolId = await this.userService.GetSchoolIdForUser(UserName);

      var school = await this.schoolService.GetSchoolData(schoolId);

      var model = new School
      {
        Id = school.Id,
        Name = school.Name,
        Address = school.Address
      };

      return View(nameof(Principal), model);
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
