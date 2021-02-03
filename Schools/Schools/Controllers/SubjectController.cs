using Microsoft.AspNetCore.Authorization;
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
      var schoolId = await this.userService.GetSchoolIdForUser(UserId);
      var subjects = await this.subjectService.GetAll(schoolId);

      return View("Index",subjects);
    }

    [HttpGet]
    public async Task<IActionResult> SubjectsForClassAndTeacher(int classId)
    {
      var schoolId = await this.userService.GetSchoolIdForUser(UserId);
      var subjects = await this.subjectService.GetAll(schoolId);

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

      return await Index();
    }

    [HttpPost]

    public async Task<IActionResult> Delete(int subjectId)
    {
      await this.subjectService.Delete(subjectId);

      return await Index();
    }
  }
}
