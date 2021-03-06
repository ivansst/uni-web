using Microsoft.EntityFrameworkCore;
using Schools.Data;
using Schools.Data.Models;
using Schools.Models.UserModels;
using Schools.Services.Interfaces;
using Schools.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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

    public async Task<StudentEditViewModel> GetViewModel(string userId)
    {
      var student = await this.data.Users.FirstOrDefaultAsync(s => s.Id == userId);

      if(student == null)
      {
        throw new Exception("User doesn't exist");
      }

      var basicEditModel = new UserEditModel
      {
        UserId = student.Id,
        FirstName = student.FirstName,
        MiddleName = student.MiddleName,
        LastName = student.LastName
      };

      var studentClass = await this.data.StudentClass.FirstOrDefaultAsync(sc => sc.StudentId == student.Id);

      var classes = await this.data.Classes.Where(c => c.SchoolId == student.SchoolId).ToListAsync();

      var model = new StudentEditViewModel
      {
        UserEditModel = basicEditModel,
        CurrentClass = studentClass.Class,
        Classes =  classes
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

    public async Task<IEnumerable<User>> GetAll(int schoolId)
    {
      var students = await this.data.Users.Where(c => c.SchoolId == schoolId && c.Role == "Student").ToListAsync();

      if (students == null)
      {
        throw new Exception("There is no school with this id!");
      }

      return students;
    }

  }
}
