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
    private readonly IClassService classService;

    public SchoolController(ISchoolService schoolService, IClassService classService)
    {
      this.schoolService = schoolService;
      this.classService = classService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {

      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(SaveSchoolRequestModel model)
    {
      if (!ModelState.IsValid)
      {
        return View();
      }

      await this.schoolService.Save(model.Id, model.Name, model.Address);

      return View();
    }
  }
}
