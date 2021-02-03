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
    private readonly ISubjectService subjectService;

    public TeacherController(ITeacherService teacherService, IUserService userService, ISubjectService subjectService)
    {
      this.teacherService = teacherService;
      this.userService = userService;
      this.subjectService = subjectService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
      var schoolId = await this.userService.GetSchoolIdForUser(UserId);

      var teachers = await this.teacherService.GetAll(schoolId);

      return View(nameof(Index), teachers);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
      var schoolId = await this.userService.GetSchoolIdForUser(UserId);

      var subjects = await this.subjectService.GetAll(schoolId);

      var model = new TeacherCreateViewModel
      {
        Subjects = subjects,
      };

      return View(nameof(Create), model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(TeacherCreateViewModel model)
    {

      var schoolId = await this.userService.GetSchoolIdForUser(UserId);

      model.SchoolId = schoolId;

      if (!ModelState.IsValid)
      {
        return View(nameof(Create));
      }

      var user = await this.userService.Create(model);

      await this.teacherService.UpdateTeacherSubjects(user.Id, model.Subjects);

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