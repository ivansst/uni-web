﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Schools.Data.Models;
using Schools.Models.UserModels;
using Schools.Services.Interfaces;
using Schools.ViewModels;
using System.Threading.Tasks;

namespace Schools.Controllers
{
  public class UserController : Controller
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
        return View();
      }

      await this.userService.Create(model.UserCreateRequestModel);

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