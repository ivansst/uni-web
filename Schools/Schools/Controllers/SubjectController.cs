using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schools.Models.SubjectModels;
using Schools.Services.Interfaces;
using System.Threading.Tasks;

namespace Schools.Controllers
{
  public class SubjectController : BaseController
  {
    private readonly IUserService userService;
    private readonly ISubjectService subjectService;

    public SubjectController(ISubjectService subjectService, IUserService userService)
    {
      this.subjectService = subjectService;
      this.userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> AllSubjects()
    {
      var schoolId = await this.userService.GetSchoolIdForUser(UserId);
      var subjects = await this.subjectService.GetAll(schoolId);

      return View("Index", subjects);
    }

    [HttpGet]
    public async Task<IActionResult> SubjectsForTeacher()
    {
      var subjects = await this.subjectService.GetSubjectsForTeacher(UserId);

      return View("Index", subjects);
    }

    [HttpGet]
    public async Task<IActionResult> SubjectsForClassAndTeacher(int classId)
    {
      var subjects = await this.subjectService.GetSubjectsForClassAndTeacher(UserId, classId);

      return View("Index", subjects);
    }

    [HttpGet]
    public async Task<IActionResult> SubjectsForClass(int classId)
    {
      var subjects = await this.subjectService.GetSubjectsForClass(classId);

      return View("Index", subjects);
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
        return View(nameof(Create));
      }

      var schoolId = await this.userService.GetSchoolIdForUser(UserId);
      if (!model.SchoolId.HasValue)
      {
        model.SchoolId = schoolId;
      }

      await this.subjectService.Create(model);

      return await AllSubjects();
    }

    [HttpPost]

    public async Task<IActionResult> Delete(int subjectId)
    {
      await this.subjectService.Delete(subjectId);

      return await AllSubjects();
    }
  }
}
