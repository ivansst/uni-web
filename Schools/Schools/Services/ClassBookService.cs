using Microsoft.EntityFrameworkCore;
using Schools.Data;
using Schools.Data.Models;
using Schools.Models.ClassBookModels;
using Schools.Services.Interfaces;
using Schools.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schools.Services
{
  public class ClassBookService : IClassBookService
  {
    private readonly ApplicationDbContext data;

    public ClassBookService(ApplicationDbContext data)
    {
      this.data = data;
    }

    public async Task AddAbsence(string userId, int value)
    {
      var student = await this.data.Users.FirstOrDefaultAsync(u => u.Id == userId);
      if (student == null)
      {
        throw new Exception("Student doesn't exist");
      }

      var absenceModel = new StudentAbsence
      {
        StudentId = userId,
        Absences = value
      };

      this.data.StudentAbsences.Add(absenceModel);

      await this.data.SaveChangesAsync();
    }

    public async Task AddGrade(string userId, int subjectId, int value)
    {
      var student = await this.data.Users.FirstOrDefaultAsync(u => u.Id == userId);
      if (student == null)
      {
        throw new Exception("Student doesn't exist");
      }

      var subject = await this.data.Subjects.FirstOrDefaultAsync(s => s.Id == subjectId);
      if (subject == null)
      {
        throw new Exception("Subject doesn't exist");
      }

      var gradeModel = new StudentGrade
      {
        StudentId = student.Id,
        SubjectId = subjectId,
        Grade = value
      };

      this.data.StudentGrades.Add(gradeModel);

      await this.data.SaveChangesAsync();
    }

    public async Task<IEnumerable<ClassBookModel>> GetViewModel(int classId, int subjectId)
    {
      var students = await this.data.StudentClass.Include(s => s.Student)
                                                 .Where(sc => sc.ClassId == classId)
                                                 .Select(sc => sc.Student)
                                                 .ToListAsync();

      var studentsIds = students.Select(s => s.Id).ToList();

      var studentsAbsences = await this.data.StudentAbsences.Where(sa => studentsIds.Contains(sa.StudentId)).ToListAsync();
      var studentsGrades = await this.data.StudentGrades.Where(sg => studentsIds.Contains(sg.StudentId) && sg.Subject.Id == subjectId).ToListAsync();

      var classBookModels = new List<ClassBookModel>();
      var subjectName = (await this.data.Subjects.FirstOrDefaultAsync(s => s.Id == subjectId))?.Name;

      foreach (var student in students)
      {
        classBookModels.Add(new ClassBookModel
        {
          UserId = student.Id,
          FirstName = student.FirstName,
          MiddleName = student.MiddleName,
          LastName = student.LastName,
          StudentGrades = studentsGrades.Where(sg => sg.StudentId == student.Id).Select(sg => sg.Grade).ToList(),
          StudentAbsences = studentsAbsences.Where(sa => sa.StudentId == student.Id).Select(sa => sa.Absences).ToList(),
          SubjectName = subjectName
        });
      }

      return classBookModels;
    }

    public async Task<StudentBookViewModel> GetStudentViewModel(string studentId)
    {
      var student = await this.data.StudentClass.Include(sc => sc.Class).FirstOrDefaultAsync(s => s.StudentId == studentId);

      var studentAbsences = await this.data.StudentAbsences.Where(sa => sa.StudentId == studentId).Select(sa => sa.Absences).ToListAsync();
      var studentGrades = await this.data.StudentGrades.Where(sg => sg.StudentId == studentId).ToListAsync();

      var subjects = await this.data.Subjects.Include(s => s.Teacher).Where(s => s.Class.Contains(student.Class)).ToListAsync();

      var classBookModels = new List<StudentBookModel>();

      var subjectGrades = new List<int>();

      foreach (var subject in subjects)
      {
        subjectGrades = studentGrades.Where(sg => sg.Subject.Id == subject.Id).Select(sg => sg.Grade).ToList();
        classBookModels.Add(new StudentBookModel
        {
          StudentGrades = subjectGrades,
          SubjectName = subject.Name,
        });
      }

      var model = new StudentBookViewModel
      {
        StudentBookModel = classBookModels,
        StudentAbsences = studentAbsences,
      };

      return model;
    }

  }
}
