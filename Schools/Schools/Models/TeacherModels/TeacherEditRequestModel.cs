using Schools.Data.Models;
using System.Collections.Generic;

namespace Schools.Models.TeacherModels
{
  public class TeacherEditRequestModel
  {
    public string FirstName { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }

    public string TeacherId { get; set; }

    public List<Subject> Subjects { get; set; }
  }
}
