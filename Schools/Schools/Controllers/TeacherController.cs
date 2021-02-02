using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
      var schoolId = await this.userService.GetSchoolIdForUser(UserName);

      var teachers = await this.teacherService.GetAll(schoolId);

      return View(nameof(Index), teachers);
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

      return await Edit(model.UserEditModel.UserId);
    }
  }
}