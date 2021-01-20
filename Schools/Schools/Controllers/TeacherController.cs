using Microsoft.AspNetCore.Mvc;
using Schools.Models.TeacherModels;
using Schools.Services.Interfaces;
using System;
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
    public IActionResult Student()
    {
      return View(nameof(Student));
    }

    [HttpGet]
    public IActionResult Teacher()
    {
        return View(nameof(Teacher));
    }

    [HttpGet]
    public IActionResult Edit()
    {
      return View(nameof(Edit));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(TeacherEditRequestModel model)
    {
      if (!ModelState.IsValid)
      {
        return View();
      }

      await this.userService.UpdatePersonalData(model.TeacherId, model.FirstName, model.MiddleName, model.LastName);

      await this.teacherService.UpdateClassSubjects(model.TeacherId, model.Subjects);

      return View();
    }
  }
}
