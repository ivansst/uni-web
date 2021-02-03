using Microsoft.AspNetCore.Mvc;
using Schools.Models.UserModels;
using Schools.Services.Interfaces;
using Schools.ViewModels;
using System.Threading.Tasks;

namespace Schools.Controllers
{
  public class StudentController : BaseController
  {
    public readonly IUserService userService;
    public readonly IClassService classService;
    public readonly IStudentService studentService;

    public StudentController(IUserService userService, IClassService classService, IStudentService studentService)
    {
      this.userService = userService;
      this.classService = classService;
      this.studentService = studentService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
      var schoolId = await this.userService.GetSchoolIdForUser(UserId);

      var students = await this.studentService.GetAll(schoolId);

      return View(nameof(Index), students);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string userId)
    {
      var model = await this.studentService.GetViewModel(userId);

      return View(nameof(Edit), model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(StudentEditViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return View(nameof(Edit));
      }

      await this.userService.UpdatePersonalData(model.UserEditModel);

      await this.studentService.SaveStudentClass(model.UserEditModel.UserId, model.NewClassId.Value);

      return await Index();
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {

      var schoolId = await this.userService.GetSchoolIdForUser(UserId);

      var classes = await this.classService.GetAll(schoolId);

      var model = new StudentCreateViewModel
      {
        Classes = classes,
      };

      return View(nameof(Create), model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(StudentCreateViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return View(nameof(Create));
      }
      var schoolId = await this.userService.GetSchoolIdForUser(UserId);

      model.SchoolId = schoolId;

      var user = await this.userService.Create(model);

      await this.classService.AddStudentToClass(user.Id, model.ClassId);

      return await Index();
    }

    [HttpGet]
    public async Task<IActionResult> StudentBook(string userId)
    {
      return View(nameof(StudentBook));
    }
  }
}
