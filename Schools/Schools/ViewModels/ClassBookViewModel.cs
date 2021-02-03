using Schools.Models.ClassBookModels;
using System.Collections.Generic;

namespace Schools.ViewModels
{
  public class ClassBookViewModel
  {
    public IEnumerable<ClassBookModel> ClassBookModels { get; set; }
  }
}
