using Schools.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schools.Models.ClassModels
{
  public class ClassSubjectEditModel : Subject
  {
    public bool IsForClass { get; set; }
  }
}
