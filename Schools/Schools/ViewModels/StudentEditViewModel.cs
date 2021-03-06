using Schools.Data.Models;
using Schools.Models.UserModels;
using System.Collections.Generic;

namespace Schools.ViewModels
{
  public class StudentEditViewModel
  {
    public UserEditModel UserEditModel { get; set; }

    public Class CurrentClass { get; set; }

    public int? NewClassId { get; set; }

    public IEnumerable<Class> Classes { get; set; }
  }
}
