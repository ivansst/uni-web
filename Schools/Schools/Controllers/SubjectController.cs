﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schools.Models.SubjectModels;
using Schools.Services.Interfaces;
using Schools.ViewModels;
using System.Threading.Tasks;

namespace Schools.Controllers
{
  [Authorize]
  public class SubjectController : BaseController
  {
    private readonly IUserService userService;
    private readonly ISubjectService subjectService;

    public SubjectController(ISubjectService subjectService, IUserService userService)
    {
      this.subjectService = subjectService;
      this.userService = userService;
    }

    public async Task<IActionResult> Index()
    {
      var schoolId = await this.userService.GetSchoolIdForUser(UserName);
      var subjects = await this.subjectService.GetAll(schoolId);

      return View(subjects);
    }

    [HttpGet]
    public IActionResult Create()
    {

      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(SubjectCreateRequestModel model)
    {

      if (!ModelState.IsValid)
      {
        return View();
      }

      var schoolId = await this.userService.GetSchoolIdForUser(UserName);
      if (!model.SchoolId.HasValue)
      {
        model.SchoolId = schoolId;
      }

      await this.subjectService.Create(model);

      return View();
    }

    [HttpPost]

    public async Task<IActionResult> Delete(int Id)
    {
      await this.subjectService.Delete(Id);

      return View();
    }
  }
}
