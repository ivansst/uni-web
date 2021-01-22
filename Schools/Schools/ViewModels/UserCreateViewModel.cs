using Schools.Data.Models;
using Schools.Models.UserModels;
using System.Collections.Generic;

namespace Schools.ViewModels
{
  public class UserCreateViewModel
  {
    public UserCreateRequestModel UserCreateRequestModel { get; set; }
    
    public IEnumerable<School> Schools { get; set; }
  }
}
