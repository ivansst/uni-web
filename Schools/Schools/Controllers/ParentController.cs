using Microsoft.AspNetCore.Mvc;
using Schools.Models.UserModels;
using Schools.Services.Interfaces;
using Schools.ViewModels;
using System.Threading.Tasks;

namespace Schools.Controllers
{
  public class ParentController : BaseController
  {
    private readonly IParentService parentService;
    private readonly IUserService userService;

    public ParentController(IParentService parentService, IUserService userService)
    {
      this.parentService = parentService;
      this.userService = userService;
    }

    public async Task<IActionResult> Index()
    {
      var schoolId = await this.userService.GetSchoolIdForUser(UserId);
      var parents = await this.parentService.GetAll(schoolId);

      return View(nameof(Index), parents);
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
    public IActionResult Edit(string userId)
    {
      return View();
    }

    [HttpPost]
    public IActionResult Edit(ParentEditViewModel model)
    {
      return View();
    }
  }
}
