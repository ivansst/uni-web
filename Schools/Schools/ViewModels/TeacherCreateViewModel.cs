using Schools.Data.Models;
using Schools.Models.UserModels;
using System.Collections.Generic;

namespace Schools.ViewModels
{
  public class TeacherCreateViewModel : UserCreateRequestModel
  {
    public IEnumerable<Subject> Subjects { get; set; }

    public IEnumerable<int> SubjectIds { get; set; } = new List<int>();
  }
}
