using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schools.Models.ClassBookModels
{
  public class AddGradeModel
  {
    public int Grade { get; set; }

    public string UserId { get; set; }

    public int SubjectId { get; set; }

    public int ClassId { get; set; }
  }
}
