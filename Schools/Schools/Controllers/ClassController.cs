using Microsoft.AspNetCore.Mvc;
using Schools.Models.ClassModels;
using Schools.Services.Interfaces;
using Schools.ViewModels;
using System.Threading.Tasks;

namespace Schools.Controllers
{
  public class ClassController : BaseController
  {
    private readonly IClassService classService;
    private readonly ISchoolService schoolService;
    private readonly IUserService userService;
    private readonly IStudentService studentService;
    private readonly ISubjectService subjectService;

    public ClassController(IClassService classService, 
                           ISchoolService schoolService, 
                           IUserService userService, 
                           IStudentService studentService,
                           ISubjectService subjectService)
    {
      this.classService = classService;
      this.schoolService = schoolService;
      this.userService = userService;
      this.studentService = studentService;
      this.subjectService = subjectService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
      var schoolId = await this.userService.GetSchoolIdForUser(UserName);

      var classes = await this.classService.GetAll(schoolId);

      return View(nameof(Index), classes);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
      var schoolId = await this.userService.GetSchoolIdForUser(UserName);

      var subjects = await this.subjectService.GetAll(schoolId);

      var model = new ClassCreateRequestModel
      {
        Subjects = subjects,
        SchoolId = schoolId
      };

      return View(nameof(Create), model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ClassCreateRequestModel model)
    {
      if (!ModelState.IsValid)
      {
        return View(nameof(Create));
      }

      await this.classService.Create(model);

      return View(nameof(Create));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {

      var model = await this.classService.GetEditModel(id);

      return View(nameof(Edit), model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ClassEditRequestModel model)
    {

      await this.classService.Edit(model);

      return RedirectToAction(nameof(Edit), new { model.Id });
    }

    [HttpGet]
    public async Task<IActionResult> AddStudentClass()
    {
      var schoolId = await this.userService.GetSchoolIdForUser(UserName);

      var students = await this.studentService.GetAll(schoolId);
      var classes = await this.classService.GetAll(schoolId);

      var model = new AddStudentClassViewModel
      {
        Students = students,
        Classes = classes
      };

      return View(nameof(AddStudentClass), model);
    }

    [HttpPost]
    public async Task<IActionResult> AddStudentClass(AddStudentClassViewModel model)
    {
      await this.classService.AddStudentToClass(model.StudentId, model.ClassId);

      return View(nameof(AddStudentClass));
    }
  }
}
