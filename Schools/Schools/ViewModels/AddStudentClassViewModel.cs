using Schools.Data.Models;
using System.Collections.Generic;

namespace Schools.ViewModels
{
  public class AddStudentClassViewModel
  {
    public IEnumerable<Class> Classes { get; set; }

    public int ClassId { get; set; }

    public IEnumerable<User> Students { get; set; }

    public string StudentId { get; set; }
  }
}
