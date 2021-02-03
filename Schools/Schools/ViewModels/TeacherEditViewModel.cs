using Schools.Data.Models;
using Schools.Models.UserModels;
using System.Collections.Generic;

namespace Schools.ViewModels
{
  public class TeacherEditViewModel
  {
    public UserEditModel UserEditModel { get; set; }

    public IEnumerable<int> NewTeacherSubjectIds { get; set; } = new List<int>();

    public IEnumerable<Subject> TeacherSubjects { get; set; }

    public IEnumerable<Subject> SchoolSubjects { get; set; } 
  }
}
