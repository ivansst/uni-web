using Microsoft.AspNetCore.Mvc;
using Schools.Models.ClassModels;
using Schools.Services.Interfaces;
using Schools.ViewModels;
using System.Threading.Tasks;

namespace Schools.Controllers
{
  public class ClassController : Controller
  {

    private readonly IClassService classService;

    public ClassController(IClassService classService)
    {
      this.classService = classService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
      var classes = await this.classService.GetAll(null);

      var model = new ClassViewModel
      {
        Classes = classes
      };

      return View(nameof(Index), model);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {

      var model = await this.classService.GetSaveViewModel(null);

      return View(nameof(Create), model);
    }

    [HttpPost]
    public async Task<IActionResult> Save(ClassSaveRequestModel model)
    {
       if (!ModelState.IsValid)
      {
        return View();
      }

      await this.classService.Save(model);

      return View();
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {

      var model = await this.classService.GetSaveViewModel(id);

      return View(nameof(Edit), model);
    }

  }
}
