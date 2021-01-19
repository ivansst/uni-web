using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Schools.Data.Models;
using Schools.Models.AccountModels;
using System.Threading.Tasks;

namespace Schools.Controllers
{
  public class AccountController : Controller
  {
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;

    public AccountController(UserManager<User> userManager,
                             SignInManager<User> signInManager)
    {
      this.userManager = userManager;
      this.signInManager = signInManager;
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

      var user = await this.userManager.FindByNameAsync(model.UserName);
      if(user == null)
      {
        return View();
      }

      var logInResult = await this.signInManager.PasswordSignInAsync(user, model.Password, false, false);
      if (!logInResult.Succeeded)
      {
        return View(nameof(Login), model);
      }

      return RedirectToAction("Index", "Dashboard");
    }

    [HttpGet]
    public IActionResult Profile()
    {
        return View(nameof(Profile));
    }

    }
}
