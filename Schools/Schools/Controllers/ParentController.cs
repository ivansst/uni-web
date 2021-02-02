using Microsoft.AspNetCore.Mvc;
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
      var schoolId = await this.userService.GetSchoolIdForUser(UserName);
      var parents = await this.parentService.GetAll(schoolId);

      return View(nameof(Index), parents);
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
