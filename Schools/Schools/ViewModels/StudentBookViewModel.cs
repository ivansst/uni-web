using Schools.Models.ClassBookModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schools.ViewModels
{
  public class StudentBookViewModel
  {
    public IEnumerable<StudentBookModel> StudentBookModel { get; set; }

    public IEnumerable<int> StudentAbsences { get; set; } = new List<int>();

  }
}
