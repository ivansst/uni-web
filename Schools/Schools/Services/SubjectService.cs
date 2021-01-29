using Microsoft.EntityFrameworkCore;
using Schools.Data;
using Schools.Data.Models;
using Schools.Models.SubjectModels;
using Schools.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schools.Services
{
  public class SubjectService : ISubjectService
  {
    private readonly ApplicationDbContext data;

    public SubjectService(ApplicationDbContext data)
    {
      this.data = data;
    }

    public async Task Create(SubjectCreateRequestModel model)
    {
      var subjectModel = new Subject
      {
        Name = model.Name,
        SchoolId = model.SchoolId
      };

      this.data.Add(subjectModel);

      await this.data.SaveChangesAsync();
    }

    public async Task Delete(int Id)
    {

      var subject = await this.data.Subjects.FirstOrDefaultAsync(s => s.Id == Id);

      if (subject == null)
      {
        throw new Exception("There is no subject with this id.");
      }

      this.data.Subjects.Remove(subject);

      await this.data.SaveChangesAsync();

    }

    public async Task<List<Subject>> GetAll(int? schoolId)
    {
      if (schoolId.HasValue)
      {
        var subjects = await this.data.Subjects.Where(c => c.SchoolId == schoolId).ToListAsync();

        return subjects;
      }
      else
      {
        var subjects = await this.data.Subjects.ToListAsync();

        return subjects;
      }
    }

  }
}
