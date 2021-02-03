using Microsoft.AspNetCore.Mvc;
using Schools.Services.Interfaces;
using Schools.ViewModels;
using System.Threading.Tasks;

namespace Schools.Controllers
{
  public class ClassBookController : BaseController
  {
    private readonly IClassBookService classBookService;

    public ClassBookController(IClassBookService classBookService)
    {
      this.classBookService = classBookService;
    }

    public async Task<IActionResult> ClassBook(int classId, int subjectId)
    {
      var classBooks = await this.classBookService.GetViewModel(classId, subjectId);

      var model = new ClassBookViewModel
      {
        ClassBookModels = classBooks
      };

      return View(nameof(ClassBook), model);
    }
  }
}
