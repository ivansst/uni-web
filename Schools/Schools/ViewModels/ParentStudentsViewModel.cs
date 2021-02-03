using Schools.Models.ParentModels;
using System.Collections.Generic;

namespace Schools.ViewModels
{
  public class ParentStudentsViewModel
  {
    public IEnumerable<ParentStudentsModel> ParentStudents { get; set; }
  }
}
