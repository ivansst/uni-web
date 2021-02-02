using Microsoft.AspNetCore.Mvc;
using Schools.Models.SchoolModels;
using Schools.Services.Interfaces;
using Schools.ViewModels;
using System.Threading.Tasks;

namespace Schools.Controllers
{
  public class AdministratorController : Controller
  {
    public readonly ITeacherService teacherService;
    public readonly IUserService userService;
    public readonly ISchoolService schoolService;
    public readonly IStudentService studentService;
    public readonly IParentService parentService;
    public readonly IClassService classService;
    public readonly ISubjectService subjectService;

    public AdministratorController(ITeacherService teacherService, IUserService userService,
      ISchoolService schoolService, IStudentService studentService, IParentService parentService,
      IClassService classService, ISubjectService subjectService)
    {
      this.teacherService = teacherService;
      this.userService = userService;
      this.schoolService = schoolService;
      this.studentService = studentService;
      this.parentService = parentService;
      this.classService = classService;
      this.subjectService = subjectService;
    }

    [HttpGet]
    public async Task<IActionResult> School(int schoolId)
    {
      ViewData["schoolId"] = schoolId;

      var schoolData = await this.schoolService.GetSchoolData(schoolId);

      var principal = await this.schoolService.GetPrincipal(schoolId);

      var model = new SchoolDataViewModel
      {
        School = schoolData,
        Principal = principal
      };

      return View(nameof(School), model);
    }

    [HttpGet]
    public async Task<IActionResult> Teachers(int schoolId)
    {
      ViewData["schoolId"] = schoolId;

      var model = await this.teacherService.GetAll(schoolId);

      return View(nameof(Teachers), model);
    }

    [HttpGet]
    public async Task<IActionResult> Students(int schoolId)
    {
      ViewData["schoolId"] = schoolId;

      var model = await this.studentService.GetAll(schoolId);

      return View(nameof(Students), model);
    }

    [HttpGet]
    public async Task<IActionResult> Parents(int schoolId)
    {
      ViewData["schoolId"] = schoolId;

      var model = await this.parentService.GetAll(schoolId);

      return View(nameof(Parents), model);
    }

    [HttpGet]
    public async Task<IActionResult> Classes(int schoolId)
    {
      ViewData["schoolId"] = schoolId;

      var model = await this.classService.GetAll(schoolId);

      return View(nameof(Classes), model);
    }

    [HttpGet]
    public async Task<IActionResult> Subjects(int schoolId)
    {
      ViewData["schoolId"] = schoolId;

      var model = await this.subjectService.GetAll(schoolId);

      return View(nameof(Subjects), model);
    }

    [HttpGet]
    public async Task<IActionResult> EditPrincipal(int schoolId)
    {
      ViewData["schoolId"] = schoolId;

      var model = await this.schoolService.GetPrincipal(schoolId);

      return View(nameof(EditPrincipal), model);
    }

    [HttpPost]
    public async Task<IActionResult> EditPrincipal(SchoolPrincipalModel model)
    {
      if (!ModelState.IsValid)
      {
        return View();
      }

      await this.userService.UpdatePersonalData(model.UserEditModel);

      return View();
    }
  }
}
