using Schools.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schools.Services.Interfaces
{
  public interface IClassService
  {
    Task Create(int name, string group, int schoolId, List<Subject> subjects);
  }
}
