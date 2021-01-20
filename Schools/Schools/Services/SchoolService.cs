using Microsoft.EntityFrameworkCore;
using Schools.Data;
using Schools.Data.Models;
using Schools.Services.Interfaces;
using System.Threading.Tasks;

namespace Schools.Services
{
  public class SchoolService : ISchoolService
  {
    private readonly ApplicationDbContext data;

    public SchoolService(ApplicationDbContext data) => this.data = data;

    public async Task AssignPrincipal(int schoolId, string userId)
    {
      var user = await this.data.Users.FirstOrDefaultAsync(u=>u.Id == userId);

      user.SchoolId = schoolId;
      user.Role = "Директор";

      await this.data.SaveChangesAsync();
    }

    public async Task Save(int id, string name, string address)
    {
      var school = await this.data.Schools.FirstOrDefaultAsync(s => s.Id == id);

      if (school == null)
      {
        school = new School
        {
          Name = name,
          Address = address
        };

        this.data.Add(school);
      }
      else
      {
        school.Name = name;
        school.Address = address;

        this.data.Update(school);
      }

      await this.data.SaveChangesAsync();
    }
  }
}