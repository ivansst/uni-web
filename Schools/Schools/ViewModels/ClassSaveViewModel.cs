using Schools.Data.Models;
using Schools.Models.ClassModels;
using System.Collections.Generic;

namespace Schools.ViewModels
{
  public class ClassSaveViewModel
  {
    public ClassSaveRequestModel ClassCreateRequestModel { get; set; }

    public IEnumerable<Subject> Subjects { get; set; }
  }
}
