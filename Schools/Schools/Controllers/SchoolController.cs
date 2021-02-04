using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Schools.Data.Models;
using Schools.Models.SchoolModels;
using Schools.Services;
using Schools.Services.Interfaces;
using Schools.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schools.Controllers
{
  public class SchoolController : BaseController
  {
    private readonly ISchoolService schoolService;
    private readonly UserManager<User> userManager;
    private readonly IScheduleService scheduleService;
    private readonly IUserService userService;

    public SchoolController(ISchoolService schoolService,
                            UserManager<User> userManager,
                            IScheduleService scheduleService,
                            IUserService userService)
    {
      this.schoolService = schoolService;
      this.userManager = userManager;
      this.scheduleService = scheduleService;
      this.userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
      var schools = await this.schoolService.GetAll();

      return View(nameof(Index), schools);
    }

    [HttpGet]
    public IActionResult Create()
    {
      return View();
    }


    [HttpGet]
    public async Task<IActionResult> Edit(int schoolId)
    {
      var school = await this.schoolService.GetSchoolData(schoolId);

      var model = new SaveSchoolRequestModel
      {
        Id = school.Id,
        Name = school.Name,
        Address = school.Address,
      };

      return View(nameof(Edit), model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(SaveSchoolRequestModel model)
    {
      if (!ModelState.IsValid)
      {
        return View();
      }

      await this.schoolService.Create(model);

      var user = await this.userManager.FindByIdAsync(UserId);

      if(user.Role == "Administrator")
      {
        return RedirectToAction("Administrator", "Dashboard");
      }

      return await Index();
    }

    [HttpPost]
    public async Task<IActionResult> Edit(SaveSchoolRequestModel model)
    {
      if (!ModelState.IsValid)
      {
        return View();
      }

      await this.schoolService.Edit(model);

      var user = await this.userManager.FindByIdAsync(UserId);

      if (user.Role == "Administrator")
      {
        return RedirectToAction("School", "Administrator", new { schoolId = user.SchoolId});
      }

      return await Index();
    }

    [HttpGet]
    public async Task<IActionResult> GetScheduleCreate() 
    {
      var schoolId = await this.userService.GetSchoolIdForUser(UserId);

      var model = await this.scheduleService.GetCreateViewModel(schoolId);

      return View("Schedule", model);
    }

    [HttpGet]
    public IActionResult ScheduleJsTest()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateSchedule(List<ScheduleModel> model)
    {
      return View();
    }
  }
}
