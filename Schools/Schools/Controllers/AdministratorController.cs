using Microsoft.AspNetCore.Mvc;
using Schools.Models.SchoolModels;
using Schools.Models.UserModels;
using Schools.Services.Interfaces;
using Schools.ViewModels;
using System.Threading.Tasks;

namespace Schools.Controllers
{
  public class AdministratorController : BaseController
  {
    public readonly ITeacherService teacherService;
    public readonly IUserService userService;
    public readonly ISchoolService schoolService;
    public readonly IStudentService studentService;
    public readonly IParentService parentService;
    public readonly IClassService classService;
    public readonly ISubjectService subjectService;
    public readonly IStatisticsService statisticsService;

    public AdministratorController(ITeacherService teacherService, IUserService userService,
      ISchoolService schoolService, IStudentService studentService, IParentService parentService,
      IClassService classService, ISubjectService subjectService, IStatisticsService statisticsService)
    {
      this.teacherService = teacherService;
      this.userService = userService;
      this.schoolService = schoolService;
      this.studentService = studentService;
      this.parentService = parentService;
      this.classService = classService;
      this.subjectService = subjectService;
      this.statisticsService = statisticsService;
    }

    [HttpGet]
    public async Task<IActionResult> School(int schoolId)
    {
      await this.userService.UpdateUserSchool(UserId, schoolId);

      var schoolData = await this.schoolService.GetSchoolData(schoolId);

      var principal = await this.schoolService.GetPrincipal(schoolId);

      var statistics = this.statisticsService.GetStatistics(schoolId);

      var model = new SchoolDataViewModel
      {
        School = schoolData,
        Principal = principal,
        Statistics = statistics,
      };

      return View(nameof(School), model);
    }

    [HttpGet]
    public IActionResult CreatePrincipal()
    {
      return View(nameof(CreatePrincipal));
    }

    [HttpPost]
    public async Task<IActionResult> CreatePrincipal(UserCreateRequestModel model)
    {

      var schoolId = await this.userService.GetSchoolIdForUser(UserId);

      model.SchoolId = schoolId;

      if (!ModelState.IsValid)
      {
        return View(nameof(CreatePrincipal));
      }

      await this.userService.Create(model);

      return await School(schoolId);
    }

    [HttpGet]
    public async Task<IActionResult> EditPrincipal(int schoolId)
    {

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
