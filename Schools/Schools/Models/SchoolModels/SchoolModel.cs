using Schools.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schools.Models.SchoolModels
{
  public class SchoolModel : School
  {
    public bool HasSchedule { get; set; }
  }
}
