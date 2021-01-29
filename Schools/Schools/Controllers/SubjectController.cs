using Microsoft.AspNetCore.Mvc;
using Schools.Models.SubjectModels;
using Schools.Services.Interfaces;
using Schools.ViewModels;
using System.Threading.Tasks;

namespace Schools.Controllers
{
  public class SubjectController : Controller
  {

    private readonly ISubjectService subjectService;

    public SubjectController(ISubjectService subjectService)
    {
      this.subjectService = subjectService;
    }

    public async Task<IActionResult> Index()
    {
      var subjects = await this.subjectService.GetAll(null);

      var model = new SubjectViewModel
      {
        Subjects = subjects,
      };

      return View();
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
