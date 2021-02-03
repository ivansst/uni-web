using Schools.Data.Models;
using Schools.Models.UserModels;
using System.Collections.Generic;

namespace Schools.ViewModels
{
  public class StudentCreateViewModel : UserCreateRequestModel
  {
    public IEnumerable<Class> Classes { get; set; }

    public int ClassId { get; set; }
  }
}
