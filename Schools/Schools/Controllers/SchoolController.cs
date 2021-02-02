using Microsoft.AspNetCore.Mvc;
using Schools.Data.Models;
using Schools.Models.SchoolModels;
using Schools.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schools.Controllers
{
  public class SchoolController : Controller
  {
    private readonly ISchoolService schoolService;

    public SchoolController(ISchoolService schoolService)
    {
      this.schoolService = schoolService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
      var schools = await this.schoolService.GetAll();

      return View(nameof(Index), schools);
    }

    [HttpGet]
    public IActionResult Create()
    {
      return View();
    }


    [HttpGet]
    public async Task<IActionResult> Edit(int schoolId)
    {
      var school = await this.schoolService.GetSchoolData(schoolId);

      var model = new SaveSchoolRequestModel
      {
        Id = school.Id,
        Name = school.Name,
        Address = school.Address,
      };

      return View(nameof(Edit), model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(SaveSchoolRequestModel model)
    {
      if (!ModelState.IsValid)
      {
        return View();
      }

      await this.schoolService.Create(model);

      return View(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(SaveSchoolRequestModel model)
    {
      if (!ModelState.IsValid)
      {
        return View();
      }

      await this.schoolService.Edit(model);

      return View(nameof(Index));
    }
  }
}
