using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Schools.Data.Models;
using Schools.Models.UserModels;
using Schools.Services.Interfaces;
using Schools.ViewModels;
using System.Threading.Tasks;

namespace Schools.Controllers
{
  public class UserController : BaseController
  {
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private readonly IUserService userService;
    private readonly ISchoolService schoolService;

    public UserController(UserManager<User> userManager,
                             SignInManager<User> signInManager,
                             IUserService userService,
                             ISchoolService schoolService)
    {
      this.userManager = userManager;
      this.signInManager = signInManager;
      this.userService = userService;
      this.schoolService = schoolService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Register()
    {
      var schools = await this.schoolService.GetAll();
      var model = new UserCreateViewModel
      {
        Schools = schools
      };

      return View(nameof(Register), model);
    }

    [HttpPost]
    public async Task<IActionResult> Register(UserCreateViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return await Register();
      }

      await this.userService.Create(model.UserCreateRequestModel);

      return await Register();
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login()
    {
      return View(nameof(Login));
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
      if (!ModelState.IsValid)
      {
        return View();
      }

      var user = await this.userManager.FindByEmailAsync(model.Email);
      if (user == null)
      {
        return View();
      }

      var logInResult = await this.signInManager.PasswordSignInAsync(user, model.Password, false, false);
      if (!logInResult.Succeeded)
      {
        return View(nameof(Login), model);
      }
      
      return RedirectToAction(user.Role, "Dashboard");
    }

    [HttpGet]
    public IActionResult Logout()
    {
      this.signInManager.SignOutAsync();
      return RedirectToAction(nameof(Login));
    }

    [HttpGet]
    public async Task<IActionResult> Edit()
    {
      var model = await this.userService.GetEditViewModel(UserId);

      return View(nameof(Edit), model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UserEditModel model)
    {
      if (!ModelState.IsValid)
      {
        return View(nameof(Edit), model);
      }

      await this.userService.UpdatePersonalData(model);

      return RedirectToAction(nameof(Edit));
    }
  }
}
