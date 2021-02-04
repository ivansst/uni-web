using Microsoft.AspNetCore.Mvc;
using Schools.Models.ScheduleModels;
using Schools.Models.SchoolModels;
using Schools.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schools.Controllers
{
  public class ScheduleController : BaseController
  {
    private readonly IScheduleService scheduleService;
    private readonly IUserService userService;

    public ScheduleController(IScheduleService scheduleService, IUserService userService)
    {
      this.scheduleService = scheduleService;
      this.userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
      var schoolId = await this.userService.GetSchoolIdForUser(UserId);

      var model = await this.scheduleService.GetSchedule(schoolId);

      return View(nameof(Index), model);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
      var schoolId = await this.userService.GetSchoolIdForUser(UserId);

      var model = await this.scheduleService.GetCreateViewModel(schoolId);

      return View(nameof(Create), model);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] IEnumerable<ScheduleCreateModel> model)
    {
      if (!ModelState.IsValid)
      {
        return View();
      }

      var schoolId = await this.userService.GetSchoolIdForUser(UserId);

      await this.scheduleService.Create(schoolId, model);

      return View(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit()
    {
      var schoolId = await this.userService.GetSchoolIdForUser(UserId);

      var model = await this.scheduleService.GetScheduleEditModel(schoolId);

      return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit([FromBody] IEnumerable<ScheduleEditModel> model)
    {
      var schoolId = await this.userService.GetSchoolIdForUser(UserId);

      await this.scheduleService.EditSchedule(schoolId, model);

      return View(nameof(Index));
    }
  }
}
