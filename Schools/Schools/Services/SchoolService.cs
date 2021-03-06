using Microsoft.EntityFrameworkCore;
using Schools.Data;
using Schools.Data.Models;
using Schools.Models.SchoolModels;
using Schools.Models.UserModels;
using Schools.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schools.Services
{
  public class SchoolService : ISchoolService
  {
    private readonly ApplicationDbContext data;

    public SchoolService(ApplicationDbContext data) => this.data = data;

    public async Task AssignNewPrincipal(int schoolId, string userId)
    {
      var user = await this.data.Users.FirstOrDefaultAsync(u => u.Id == userId && u.SchoolId == schoolId);

      user.Role = "Principal";

      await this.data.SaveChangesAsync();
    }

    public async Task<IEnumerable<School>> GetAll()
    {
      return await this.data.Schools.ToListAsync();
    }

    public async Task<SchoolPrincipalModel> GetPrincipal(int schoolId)
    {
      var user = await this.data.Users.FirstOrDefaultAsync(u => u.SchoolId == schoolId && u.Role == "Principal");

      if (user == null)
      {
        return new SchoolPrincipalModel
        {
          Role = null,
        };
      }

      var schoolName = (await this.data.Schools.FirstOrDefaultAsync(s => s.Id == user.SchoolId)).Name;

      var userEditModel = new UserEditModel
      {
        UserId = user.Id,
        FirstName = user.FirstName,
        MiddleName = user.MiddleName,
        LastName = user.LastName
      };

      var model = new SchoolPrincipalModel
      {
        UserEditModel = userEditModel,
        Role = user.Role,
        SchoolName = schoolName
      };

      return model;
    }

    public async Task RemoveUserFromSchool(int schoolId, string userId)
    {
      var user = await this.data.Users.FirstOrDefaultAsync(u => u.Id == userId && u.SchoolId == schoolId);

      if (user == null)
      {
        throw new Exception("There is no such user attached to this school");
      }

      user.SchoolId = null;
      user.Role = null;

      await this.data.SaveChangesAsync();
    }

    public async Task Create(SaveSchoolRequestModel model)
    {
      var school = new School
      {
        Name = model.Name,
        Address = model.Address
      };

      this.data.Add(school);

      await this.data.SaveChangesAsync();
    }

    public async Task Edit(SaveSchoolRequestModel model)
    {
      var school = await this.data.Schools.FirstOrDefaultAsync(s => s.Id == model.Id);

      school.Name = model.Name;
      school.Address = model.Address;

      this.data.Update(school);

      await this.data.SaveChangesAsync();
    }

    public async Task<SchoolModel> GetSchoolData(int schoolId)
    {
      var school = await this.data.Schools.FirstOrDefaultAsync(s => s.Id == schoolId);

      if (school == null)
      {
        throw new Exception("There is no school with this id!");
      }

      var hasSchedule = await this.data.Schedules.FirstOrDefaultAsync(s => s.SchoolId == school.Id) != null;

      var model = new SchoolModel
      {
        Id = school.Id,
        Name = school.Name,
        Address = school.Address,
        HasSchedule = hasSchedule,
      };

      return model;
    }
  }
}