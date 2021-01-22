using Microsoft.EntityFrameworkCore;
using Schools.Data;
using Schools.Services.Interfaces;
using Schools.ViewModels;
using System.Threading.Tasks;

namespace Schools.Services
{
  public class StudentService : IStudentService
  {
    private readonly ApplicationDbContext data;

    public StudentService(ApplicationDbContext data)
    {
      this.data = data;
    }

    public async Task EditStudent(StudentEditViewModel model)
    {
      var student = await this.data.Users.FirstOrDefaultAsync(u => u.Id == model.UserEditModel.UserId);

      if (student.FirstName == model.UserEditModel.FirstName &&
          student.MiddleName == model.UserEditModel.MiddleName &&
          student.LastName == model.UserEditModel.LastName)
      {
        student.FirstName = model.UserEditModel.FirstName;
        student.MiddleName = model.UserEditModel.MiddleName;
        student.LastName = model.UserEditModel.LastName;
      }

      var studentClass = await this.data.StudentClass.FirstOrDefaultAsync(sc => sc.StudentId == student.Id);

      if(studentClass.ClassId != model.ClassId)
      {
        studentClass.ClassId = model.ClassId;
      }

      await this.data.SaveChangesAsync();
    }
  }
}
