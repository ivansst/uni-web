using Microsoft.EntityFrameworkCore;
using Schools.Data;
using Schools.Data.Models;
using Schools.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schools.Services
{
  public class ClassService : IClassService
  {
    private readonly ApplicationDbContext data;

    public ClassService(ApplicationDbContext data)
    {
      this.data = data;
    }

    public async Task AddStudentToClass(string studentId, int classId)
    {
      var studentClass = new StudentClass
      {
        ClassId = classId,
        StudentId = studentId
      };

      this.data.Add(studentClass);

      await this.data.SaveChangesAsync();
    }

    public async Task Create(int name, string group, int schoolId, List<Subject> subjects)
    {
      if (name == default(int))
      {
        throw new Exception("Cannot create Class without Name");
      }

      var classModel = new Class
      {
        Name = name,
        Group = group,
        SchoolId = schoolId,
        Subject = subjects
      };

      this.data.Add(classModel);

      await this.data.SaveChangesAsync();
    }

    public async Task RemoveStudentFromClass(string studentId, int classId)
    {
      var studentClass = await this.data.StudentClass.FirstOrDefaultAsync(sc => sc.StudentId == studentId && sc.ClassId == classId);

      if (studentClass == null)
      {
        throw new Exception("There is no student that is in this class.");
      }

      this.data.StudentClass.Remove(studentClass);

      await this.data.SaveChangesAsync();
    }
  }
}
