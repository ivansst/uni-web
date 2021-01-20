using Schools.Data;
using Schools.Data.Models;
using Schools.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schools.Services
{
  public class ClassService : IClassService
  {
    private readonly ApplicationDbContext data;

    public ClassService(ApplicationDbContext data)
    {
      this.data = data;
    }

    public async Task Create(int name, string group, int schoolId, List<Subject> subjects)
    {
      if (name == default(int))
      {
        throw new Exception("Cannot create Class without Name");
      }

      var classModel = new Class
      {
        Name = name,
        Group = group,
        SchoolId = schoolId,
        Subject = subjects
      };

      this.data.Add(classModel);

      await this.data.SaveChangesAsync();
    }
  }
}
