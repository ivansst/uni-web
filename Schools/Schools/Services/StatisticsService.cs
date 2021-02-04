using Schools.Data;
using Schools.Models.SchoolModels;
using Schools.Services.Interfaces;
using System.Linq;

namespace Schools.Services
{
  public class StatisticsService : IStatisticsService
  {
    private readonly ApplicationDbContext data;

    public StatisticsService(ApplicationDbContext data)
    {
      this.data = data;
    }
    public SchoolStatisticModel GetStatistics(int schoolId)
    {
      var teacherCount = this.data.Users.Where(t => t.SchoolId == schoolId && t.Role == "Teacher").Count();

      var studentCount = this.data.Users.Where(s => s.SchoolId == schoolId && s.Role == "Student").Count();

      var parentsCount = this.data.Users.Where(p => p.SchoolId == schoolId && p.Role == "Parent").Count();

      var classesCount = this.data.Classes.Where(c => c.SchoolId == schoolId).Count();

      var subjectsCount = this.data.Classes.Where(s => s.SchoolId == schoolId).Count();

      var absents = this.data.StudentAbsences.Select(sa => sa.Absences).ToList().Sum();

      var grades = this.data.StudentGrades.Select(sg => sg.Grade).ToList();

      var gradeAverage = (double)(grades.Sum()) / grades.Count();

      if (grades.Count == 0)
      {
        gradeAverage = 0;
      }

      var model = new SchoolStatisticModel
      {
        TeacherCount = teacherCount,
        StudentCount = studentCount,
        ParentCount = parentsCount,
        ClassesCount = classesCount,
        SubjectsCount = subjectsCount,
        Absences = absents,
        GradeAverage = gradeAverage,
      };

      return model;
    }
  }
}
