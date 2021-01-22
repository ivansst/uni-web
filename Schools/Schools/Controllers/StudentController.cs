using Microsoft.AspNetCore.Mvc;
using Schools.Services.Interfaces;
using Schools.ViewModels;
using System.Threading.Tasks;

namespace Schools.Controllers
{
  public class StudentController : Controller
  {
    public readonly IUserService userService;
    public readonly IClassService classService;

    public StudentController(IUserService userService, IClassService classService)
    {
      this.userService = userService;
      this.classService = classService;
    }

    [HttpGet]
    public IActionResult Edit()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Edit(StudentEditViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return View();
      }

      await this.userService.UpdatePersonalData(model.UserEditModel.UserId, model.UserEditModel.FirstName, model.UserEditModel.MiddleName, model.UserEditModel.LastName);

      await this.classService.AddStudentToClass(model.UserEditModel.UserId, model.ClassId);

      return View();
    }
  }
}
