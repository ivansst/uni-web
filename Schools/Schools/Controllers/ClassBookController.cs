using Microsoft.AspNetCore.Mvc;
using Schools.Models.ClassBookModels;
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
        ClassBookModels = classBooks,
        ClassId = classId,
        SubjectId = subjectId,
      };

      return View(nameof(ClassBook), model);
    }

    [HttpPost]
    public async Task<IActionResult> AddGrade(AddGradeModel model)
    {
      if (!ModelState.IsValid)
      {
        return await ClassBook(model.ClassId, model.SubjectId);
      }

      await this.classBookService.AddGrade(model.UserId, model.SubjectId, model.Grade);

      return await ClassBook(model.ClassId, model.SubjectId);
    }

    [HttpPost]
    public async Task<IActionResult> AddAbsence(AddAbsenceModel model)
    {
      if (!ModelState.IsValid)
      {
        return await ClassBook(model.ClassId, model.SubjectId);
      }

      await this.classBookService.AddAbsence(model.UserId, model.AbsenceValue);

      return await ClassBook(model.ClassId, model.SubjectId);
    }
  }
}
