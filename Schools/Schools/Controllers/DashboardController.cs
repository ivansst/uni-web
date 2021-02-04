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
    private readonly IClassBookService classBookService;
    private readonly IScheduleService scheduleService;

    public DashboardController(ISchoolService schoolService, IUserService userService,
      IParentService parentService, IClassBookService classBookService, IScheduleService scheduleService)
    {
      this.userService = userService;
      this.schoolService = schoolService;
      this.parentService = parentService;
      this.classBookService = classBookService;
      this.scheduleService = scheduleService;
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
    public async Task<IActionResult> Teacher()
    {
      var schoolId = await this.userService.GetSchoolIdForUser(UserId);

      var model = await this.scheduleService.GetSchedule(schoolId);

      return View(nameof(Teacher), model);
    }

    [HttpGet]
    public async Task<IActionResult> Student()
    {
      var model = await this.classBookService.GetStudentViewModel(UserId);
      return View(nameof(Student), model);
    }

    [HttpGet]
    public async Task<IActionResult> Parent()
    {
      var model = await this.parentService.GetParentStudentsViewModel(UserId);

      return View(nameof(Parent), model);
    }

  }
}
