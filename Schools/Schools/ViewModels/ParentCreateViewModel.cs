using Schools.Data.Models;
using Schools.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schools.ViewModels
{
  public class ParentCreateViewModel : UserCreateRequestModel
  {
    public IEnumerable<User> Students { get; set; }
  }
}
