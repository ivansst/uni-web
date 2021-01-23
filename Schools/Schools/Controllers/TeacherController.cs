using Microsoft.AspNetCore.Mvc;
using Schools.Services.Interfaces;
using Schools.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace Schools.Controllers
{
  public class TeacherController : Controller
  {
    private readonly ITeacherService teacherService;
    private readonly IUserService userService;

    public TeacherController(ITeacherService teacherService, IUserService userService)
    {
      this.teacherService = teacherService;
      this.userService = userService;
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

      await this.teacherService.UpdateClassSubjects(model.UserEditModel.UserId, model.TeacherSubjects.ToList());

      return View();
    }
  }
}