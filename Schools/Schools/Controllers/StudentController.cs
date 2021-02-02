using Microsoft.AspNetCore.Mvc;
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
        return View();
      }

      await this.userService.UpdatePersonalData(model.UserEditModel);

      await this.studentService.SaveStudentClass(model.UserEditModel.UserId, model.NewClassId.Value);

      return View();
    }

  }
}
