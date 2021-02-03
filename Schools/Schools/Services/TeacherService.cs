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
  public class TeacherService : ITeacherService
  {
    private readonly ApplicationDbContext data;

    public TeacherService(ApplicationDbContext data)
    {
      this.data = data;
    }

    public async Task<IEnumerable<User>> GetAll(int schoolId)
    {
      var teachers = await this.data.Users.Where(t => t.SchoolId == schoolId && t.Role == "Teacher").ToListAsync();

      return teachers;
    }

    public async Task<TeacherEditViewModel> GetTeacherEditViewModel(string teacherId)
    {
      var teacher = await this.data.Users.Include(s => s.Subject).FirstOrDefaultAsync(t => t.Id == teacherId);

      var teacherModel = new UserEditModel
      {
        UserId = teacher.Id,
        FirstName = teacher.FirstName,
        MiddleName = teacher.MiddleName,
        LastName = teacher.LastName
      };

      var teacherSubjects = teacher.Subject;
      if (teacherSubjects == null)
      {
        teacherSubjects = new List<Subject>();
      }

      var schoolSubjects = await this.data.Subjects.Where(s => s.SchoolId == teacher.SchoolId).ToListAsync();

      var model = new TeacherEditViewModel
      {
        UserEditModel = teacherModel,
        SchoolSubjects = schoolSubjects,
        TeacherSubjects = teacherSubjects
      };

      return model;
    }

    public async Task UpdateTeacherSubjects(string teacherId, IEnumerable<int> subjectIds)
    {
      if (string.IsNullOrEmpty(teacherId))
      {
        throw new Exception("TeacehrId is required");
      }

      if (!subjectIds.Any())
      {
        subjectIds = new List<int>();
      }

      var teacher = await this.data.Users.Include(t => t.Subject).FirstOrDefaultAsync(u => u.Id == teacherId);

      var subjects = await this.data.Subjects.Where(s => subjectIds.Contains(s.Id)).ToListAsync();

      teacher.Subject = subjects;

      await this.data.SaveChangesAsync();
    }
  }
}
