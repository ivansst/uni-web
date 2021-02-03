using Schools.Models.UserModels;
using System.Collections.Generic;

namespace Schools.Models.ClassBookModels
{
  public class ClassBookModel : UserEditModel
  {
    public string SubjectName { get; set; }

    public IEnumerable<int> StudentGrades { get; set; } = new List<int>();

    public IEnumerable<int> StudentAbsences { get; set; } = new List<int>();
  }
}
