using Microsoft.AspNetCore.Mvc;
using Schools.Models.SchoolModels;
using Schools.Services.Interfaces;
using System.Threading.Tasks;

namespace Schools.Controllers
{
  public class AdministratorController : Controller
  {
    public readonly ITeacherService teacherService;
    public readonly IUserService userService;
    public readonly ISchoolService schoolService;

    public AdministratorController(ITeacherService teacherService, IUserService userService, ISchoolService schoolService)
    {
      this.teacherService = teacherService;
      this.userService = userService;
      this.schoolService = schoolService;
    }

    [HttpGet]
    public async Task<IActionResult> EditPrincipal(int schoolId)
    {
      var model = await this.schoolService.GetPrincipal(schoolId);

      return View(nameof(EditPrincipal), model);
    }

    [HttpPost]
    public async Task<IActionResult> EditPrincipal(SchoolPrincipalModel model)
    {
      if (!ModelState.IsValid)
      {
        return View();
      }

      await this.userService.UpdatePersonalData(model.UserEditModel);

      return View();
    }
  }
}
