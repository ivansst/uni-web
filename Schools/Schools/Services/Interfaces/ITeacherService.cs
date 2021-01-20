using Schools.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schools.Services.Interfaces
{
  public interface ITeacherService
  {
    Task UpdateClassSubjects(string teacherId, List<Subject> subjects);
  }
}
