using Microsoft.AspNetCore.Mvc;
using Schools.Models.ClassModels;
using Schools.Services.Interfaces;
using Schools.ViewModels;
using System.Threading.Tasks;

namespace Schools.Controllers
{
  public class ClassController : BaseController
  {
    private readonly IClassService classService;
    private readonly ISchoolService schoolService;
    private readonly IUserService userService;
    public ClassController(IClassService classService, ISchoolService schoolService, IUserService userService)
    {
      this.classService = classService;
      this.schoolService = schoolService;
      this.userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
      var schoolId = await this.userService.GetSchoolIdForUser(UserName);

      var classes = await this.classService.GetAll(schoolId);

      return View(nameof(Index), classes);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {

      var model = await this.classService.GetSaveViewModel();

      return View(nameof(Create), model);
    }

    [HttpPost]
    public async Task<IActionResult> Save(ClassSaveRequestModel model)
    {
      if (!ModelState.IsValid)
      {
        return View(nameof(Create));
      }

      await this.classService.Save(model);

      return View(nameof(Create));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {

      var model = await this.classService.GetSaveViewModel(id);

      return View(nameof(Edit), model);
    }

  }
}
