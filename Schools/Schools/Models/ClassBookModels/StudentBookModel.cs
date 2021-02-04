using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schools.Models.ClassBookModels
{
  public class StudentBookModel
  {
    public string SubjectName { get; set; }

    public IEnumerable<int> StudentGrades { get; set; } = new List<int>();

  }
}
