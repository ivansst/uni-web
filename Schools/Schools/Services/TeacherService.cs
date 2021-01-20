using Microsoft.EntityFrameworkCore;
using Schools.Data;
using Schools.Data.Models;
using Schools.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schools.Services
{
  public class TeacherService : ITeacherService
  {
    private readonly ApplicationDbContext data;

    public TeacherService(ApplicationDbContext data)
    {
      this.data = data;
    }

    public async Task UpdateClassSubjects(string teacherId, List<Subject> subjects)
    {
      if (string.IsNullOrEmpty(teacherId))
      {
        throw new Exception("TeacehrId is required");
      }

      if (!subjects.Any())
      {
        subjects = new List<Subject>();
      }

      await this.data.SaveChangesAsync();
    }
  }
}
