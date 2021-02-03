using Schools.Data.Models;
using Schools.Models.UserModels;
using System.Collections.Generic;

namespace Schools.ViewModels
{
  public class ParentEditViewModel
  {
    public UserEditModel UserEditModel { get; set; }

    public IEnumerable<User> ParentStudents { get; set; }

    public IEnumerable<User> AllStudents { get; set; }

    public IEnumerable<string> StudentIds { get; set; } = new List<string>();
  }
}
