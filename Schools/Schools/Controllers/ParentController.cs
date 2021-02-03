using Microsoft.AspNetCore.Mvc;
using Schools.Models.UserModels;
using Schools.Services.Interfaces;
using Schools.ViewModels;
using System.Threading.Tasks;

namespace Schools.Controllers
{
  public class ParentController : BaseController
  {
    private readonly IParentService parentService;
    private readonly IUserService userService;
    private readonly IStudentService studentService;

    public ParentController(IParentService parentService, IUserService userService, IStudentService studentService)
    {
      this.parentService = parentService;
      this.userService = userService;
      this.studentService = studentService;
    }

    public async Task<IActionResult> Index()
    {
      var schoolId = await this.userService.GetSchoolIdForUser(UserId);
      var parents = await this.parentService.GetAll(schoolId);

      return View(nameof(Index), parents);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
      var schoolId = await this.userService.GetSchoolIdForUser(UserId);

      var students = await this.studentService.GetAll(schoolId);

      var model = new ParentCreateViewModel
      {
        Students = students,
      };

      return View(nameof(Create), model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ParentCreateViewModel model)
    {

      var schoolId = await this.userService.GetSchoolIdForUser(UserId);

      model.SchoolId = schoolId;

      if (!ModelState.IsValid)
      {
        return View(nameof(Create));
      }

      var user = await this.userService.Create(model);

      await this.parentService.EditParentStudents(user.Id, model.Students);

      return await Index();
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string userId)
    {
      var schoolId = await this.userService.GetSchoolIdForUser(UserId);

      var parentStudents = await this.parentService.GetParentStudents(userId);

      var allStudents = await this.studentService.GetAll(schoolId);

      var model = new ParentEditViewModel
      {
        ParentStudents = parentStudents,
        AllStudents = allStudents,
      };

      return View(nameof(Edit), model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ParentEditViewModel model)
    {
      if(!ModelState.IsValid)
      {
        return View();
      }

      await this.userService.UpdatePersonalData(model.UserEditModel);

      await this.parentService.EditParentStudents(model.UserEditModel.UserId, model.ParentStudents);

      return View();
    }
  }
}
