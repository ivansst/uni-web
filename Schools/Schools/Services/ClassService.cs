using Microsoft.EntityFrameworkCore;
using Schools.Data;
using Schools.Data.Models;
using Schools.Models.ClassModels;
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

    public async Task Edit(ClassEditRequestModel model)
    {
      if (model == null)
      {
        throw new Exception("Model cannot be null");
      }

      var @class = await this.data.Classes.Include(s=> s.Subject).FirstOrDefaultAsync(c => c.Id == model.Id);

      if(@class == null)
      {
        throw new Exception("This class doesn't exist");
      }

      @class.Name = model.Name;
      @class.Group = model.Group;

      var subjects = await this.data.Subjects.Where(s => s.SchoolId == @class.SchoolId && model.SubjectIds.Contains(s.Id)).ToListAsync();
      @class.Subject = subjects;

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

    public async Task<IEnumerable<Class>> GetAll(int schoolId)
    {
      var classes = await this.data.Classes.Where(c => c.SchoolId == schoolId).ToListAsync();

      return classes;
    }

    public async Task Create(ClassCreateRequestModel model)
    {
      if (model == null)
      {
        throw new Exception("Model cannot be null");
      }

      var subjects = await this.data.Subjects.Where(s => model.SubjectIds.Contains(s.Id)).ToListAsync();

      var @class = new Class
      {
        Name = model.Name,
        Group = model.Group,
        SchoolId = model.SchoolId,
        Subject = subjects
      };

      this.data.Classes.Add(@class);

      await this.data.SaveChangesAsync();
    }

    public async Task<ClassEditRequestModel> GetEditModel(int classId)
    {
      var @class = await this.data.Classes.Include(c=> c.Subject).FirstOrDefaultAsync(c => c.Id == classId);
      var subjects = await this.data.Subjects.Include(s => s.Class).Where(s => s.SchoolId == @class.SchoolId).ToListAsync();

      if(@class == null)
      {
        throw new Exception("This class doesn't exist");
      }

      var classSubjectEditModel = new ClassSubjectEditModel();
      var classSubjects = new List<ClassSubjectEditModel>();

      foreach (var subject in subjects)
      {
        classSubjectEditModel = new ClassSubjectEditModel
        {
          Id = subject.Id,
          Name = subject.Name,
          SchoolId = subject.SchoolId
        };

        classSubjectEditModel.IsForClass = subject.Class.Contains(@class);

        classSubjects.Add(classSubjectEditModel);
      }

      var model = new ClassEditRequestModel
      {
        Id = @class.Id,
        Name = @class.Name,
        Group = @class.Group,
        AllSubjects = classSubjects
      };

      return model;
    }
  }
}
