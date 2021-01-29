using Microsoft.EntityFrameworkCore;
using Schools.Data;
using Schools.Data.Models;
using Schools.Models.ClassModels;
using Schools.Services.Interfaces;
using Schools.ViewModels;
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

    public async Task Save(ClassSaveRequestModel model)
    {

      if(model.Id == null)
      {
        if (model.Name == default(int))
        {
          throw new Exception("Cannot create Class without Name");
        }

        var classModel = new Class
        {
          Name = model.Name,
          Group = model.Group,
          SchoolId = model.SchoolId,
          Subject = model.Subjects
        };

        this.data.Add(classModel);

        await this.data.SaveChangesAsync();
      }
      else
      {
        var classData = await this.data.Classes.FirstOrDefaultAsync(s => s.Id == model.Id);

        if(classData == null)
        {
          throw new Exception("Cannot create Class without Name");
        }

        classData.Name = model.Name;
        classData.Group = model.Group;
        classData.Subject = model.Subjects;

        this.data.Update(classData);

      }
      
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

    public async Task<ClassSaveViewModel> GetSaveViewModel(int? classId)
    {
      if (classId.HasValue)
      {
        var subjects = await this.data.Subjects.ToListAsync();

        var classData = await this.data.Classes.FirstOrDefaultAsync(s => s.Id == classId);

        var basicSaveModel = new ClassSaveRequestModel
        {
          Id = classData.Id,
          Name = classData.Name,
          Group = classData.Group,
          SchoolId = classData.SchoolId,
          Subjects = subjects
        };

        var model = new ClassSaveViewModel
        {
          ClassCreateRequestModel = basicSaveModel,
          Subjects = subjects
        };

        return model;
      }
      else
      {
        var subjects = await this.data.Subjects.ToListAsync();

        var model = new ClassSaveViewModel
        {
          ClassCreateRequestModel = new ClassSaveRequestModel(),
          Subjects = subjects,
        };

        return model;
      }

    }

    public async Task<List<Class>> GetAll(int? schoolId)
    {

      if (schoolId.HasValue)
      {
        var classes = await this.data.Classes.Where(c => c.SchoolId == schoolId).ToListAsync();

        return classes;
      }
      else
      {
        var classes = await this.data.Classes.ToListAsync();

        return classes;
      }
     
    }
  }
}
