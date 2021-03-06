using Microsoft.EntityFrameworkCore;
using Schools.Data;
using Schools.Data.Models;
using Schools.Models.SubjectModels;
using Schools.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schools.Services
{
  public class SubjectService : ISubjectService
  {
    private readonly ApplicationDbContext data;

    public SubjectService(ApplicationDbContext data)
    {
      this.data = data;
    }

    public async Task Create(SubjectCreateRequestModel model)
    {
      var subjectModel = new Subject
      {
        Name = model.Name,
        SchoolId = model.SchoolId.Value
      };

      this.data.Add(subjectModel);

      await this.data.SaveChangesAsync();
    }

    public async Task Delete(int Id)
    {
      var subject = await this.data.Subjects.FirstOrDefaultAsync(s => s.Id == Id);

      if (subject == null)
      {
        throw new Exception("There is no subject with this id.");
      }

      this.data.Subjects.Remove(subject);

      await this.data.SaveChangesAsync();

    }

    public async Task<IEnumerable<SubjectModel>> GetSubjectsForTeacher(string userId)
    {
      var teacher = await this.data.Users.Include(u => u.Subject).FirstOrDefaultAsync(t => t.Id == userId);

      if (teacher == null)
      {
        throw new Exception("Teacher doesn't exist!");
      }
      var subjectsResult = MapSubjects(teacher.Subject);

      return subjectsResult;
    }

    public async Task<IEnumerable<SubjectModel>> GetAll(int schoolId)
    {
      var subjects = await this.data.Subjects.Where(c => c.SchoolId == schoolId).ToListAsync();

      var subjectsResult = MapSubjects(subjects);

      return subjectsResult;
    }

    private static IEnumerable<SubjectModel> MapSubjects(IEnumerable<Subject> subjects)
    {
      var subjectsResult = new List<SubjectModel>();

      foreach (var subject in subjects)
      {
        subjectsResult.Add(new SubjectModel
        {
          Id = subject.Id,
          Name = subject.Name,
          SchoolId = subject.SchoolId
        });
      }

      return subjectsResult.ToList();
    }

    public async Task<IEnumerable<SubjectModel>> GetSubjectsForClassAndTeacher(string userId, int classId)
    {
      var teacher = await this.data.Users.Include(t => t.Subject).FirstOrDefaultAsync(t => t.Id == userId);
      if (teacher == null)
      {
        throw new Exception("Teacher doesn't exist!");
      }

      var teacherSubjects = teacher.Subject.Any() ? teacher.Subject : new List<Subject>();

      var @class = await this.data.Classes.Include(c => c.Subject).FirstOrDefaultAsync(c => c.Id == classId);
      if (@class == null)
      {
        throw new Exception("Class doesn't exist");
      }

      var classSubjects = @class.Subject.Any() ? @class.Subject : new List<Subject>();

      var subjectModel = new SubjectModel();
      var subjects = new List<SubjectModel>();

      foreach (var subject in teacherSubjects)
      {
        if (classSubjects.Contains(subject))
        {
          subjectModel = new SubjectModel
          {
            Id = subject.Id,
            Name = subject.Name,
            SchoolId = subject.SchoolId,
            ClassId = @class.Id
          };

          subjects.Add(subjectModel);
        }
      }

      return subjects;
    }

    public async Task<IEnumerable<SubjectModel>> GetSubjectsForClass(int classId)
    {
      var @class = await this.data.Classes.Include(c => c.Subject).FirstOrDefaultAsync(c => c.Id == classId);
      if (@class == null)
      {
        throw new Exception("Class doesn't exist");
      }

      var classSubjects = @class.Subject.Any() ? @class.Subject : new List<Subject>();

      var subjectModel = new SubjectModel();
      var subjects = new List<SubjectModel>();

      foreach (var subject in classSubjects)
      {
        subjectModel = new SubjectModel
        {
          Id = subject.Id,
          Name = subject.Name,
          SchoolId = subject.SchoolId,
          ClassId = @class.Id
        };

        subjects.Add(subjectModel);
      }

      return subjects;
    }
  }
}