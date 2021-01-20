using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Schools.Data.Models;
using Schools.Models.UserModels;
using Schools.Models.UserModels.UserRequestModel;
using Schools.Services.Interfaces;
using System.Threading.Tasks;

namespace Schools.Controllers
{
  public class UserController : Controller
  {
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private readonly IUserService userService;

    public UserController(UserManager<User> userManager,
                             SignInManager<User> signInManager,
                             IUserService userService)
    {
      this.userManager = userManager;
      this.signInManager = signInManager;
      this.userService = userService;
    }

    [HttpGet]
    public IActionResult Register()
    {
      return View(nameof(Register));
    }

    [HttpPost]
    public async Task<IActionResult> Register(UserCreateRequestModel model)
    {
      if (!ModelState.IsValid)
      {
        return View();
      }

      await this.userService.Create(model);

      return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
      return View(nameof(Login));
    }

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

      if(user.Role == "Учител")
      {
        return RedirectToAction("Index", "Teacher");
      }

      return RedirectToAction("Index", "Student");
    }
  }
}
