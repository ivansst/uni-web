using Microsoft.EntityFrameworkCore;
using Schools.Data;
using Schools.Data.Models;
using Schools.Models.UserModels;
using Schools.Services.Interfaces;
using Schools.ViewModels;
using System;
using System.Threading.Tasks;

namespace Schools.Services
{
  public class StudentService : BaseService, IStudentService
  {
    private readonly ApplicationDbContext data;

    public StudentService(ApplicationDbContext data)
    {
      this.data = data;
    }

    public async Task<StudentEditViewModel> GetViewModel(string userId)
    {
      var student = await this.data.Users.FirstOrDefaultAsync(s => s.Id == userId);

      if(student == null)
      {
        throw new Exception("User doesn't exist");
      }

      var basicEditModel = new UserEditModel
      {
        FirstName = student.FirstName,
        MiddleName = student.MiddleName,
        LastName = student.LastName
      };

      var studentClass = await this.data.StudentClass.FirstOrDefaultAsync(sc => sc.StudentId == student.Id);

      var model = new StudentEditViewModel
      {
        UserEditModel = basicEditModel,
        CurrentClass = studentClass.Class
      };

      return model;
    }

    public async Task SaveStudentClass(string studentId, int classId)
    {
      var studentClass = await this.data.StudentClass.FirstOrDefaultAsync(sc => sc.StudentId == studentId);

      if (studentClass.ClassId != classId)
      {
        studentClass.ClassId = classId;
      }

      if (studentClass == null)
      {
        studentClass = new StudentClass
        {
          StudentId = studentId,
          ClassId = classId
        };

        this.data.StudentClass.Add(studentClass);
      }

      await this.data.SaveChangesAsync();
    }
  }
}
