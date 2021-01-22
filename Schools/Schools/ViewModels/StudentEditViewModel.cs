using Schools.Data.Models;
using Schools.Models.UserModels;
using System.Collections.Generic;

namespace Schools.ViewModels
{
  public class StudentEditViewModel
  {
    public BasicUserEditModel UserEditModel { get; set; }

    public int ClassId { get; set; }

    public IEnumerable<Class> Classes { get; set; }
  }
}
