using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schools.Models.UserModels;
using Schools.Services.Interfaces;
using Schools.ViewModels;
using System.Threading.Tasks;

namespace Schools.Controllers
{
  [Authorize]
  public class TeacherController : BaseController
  {
    private readonly ITeacherService teacherService;
    private readonly IUserService userService;

    public TeacherController(ITeacherService teacherService, IUserService userService)
    {
      this.teacherService = teacherService;
      this.userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
      var schoolId = await this.userService.GetSchoolIdForUser(UserId);

      var teachers = await this.teacherService.GetAll(schoolId);

      return View(nameof(Index), teachers);
    }

    [HttpGet]
    public IActionResult Create()
    {
      return View(nameof(Create));
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserCreateRequestModel model)
    {

      var schoolId = await this.userService.GetSchoolIdForUser(UserId);

      model.SchoolId = schoolId;

      if (!ModelState.IsValid)
      {
        return View(nameof(Create));
      }

      await this.userService.Create(model);

      return await Index();
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string userId)
    {
      var model = await this.teacherService.GetTeacherEditViewModel(userId);

      return View(nameof(Edit), model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(TeacherEditViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return View();
      }

      await this.userService.UpdatePersonalData(model.UserEditModel);

      await this.teacherService.UpdateTeacherSubjects(model.UserEditModel.UserId, model.NewTeacherSubjectIds);

      return await Index();
    }

  }
}