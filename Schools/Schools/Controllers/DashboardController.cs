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
    private readonly IParentService parentService;

    public DashboardController(ISchoolService schoolService, IUserService userService, IParentService parentService)
    {
      this.userService = userService;
      this.schoolService = schoolService;
      this.parentService = parentService;
    }

    [HttpGet]
    public async Task<IActionResult> Administrator()
    {
      await this.userService.UpdateUserSchool(UserId);

      var model = await this.schoolService.GetAll();

      return View(nameof(Administrator), model);
    }

    [HttpGet]
    public async Task<IActionResult> Principal()
    {

      var schoolId = await this.userService.GetSchoolIdForUser(UserId);

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
    public async Task<IActionResult> Parent()
    {
      var model = await this.parentService.GetParentStudentsViewModel(UserId);

      return View(nameof(Parent), model);
    }

  }
}
