using Microsoft.EntityFrameworkCore;
using Schools.Data;
using Schools.Data.Models;
using Schools.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Schools.Services
{
  public class SchoolService : ISchoolService
  {
    private readonly ApplicationDbContext data;

    public SchoolService(ApplicationDbContext data) => this.data = data;

    public async Task Create(string name, string address)
    {
      var school = new School
      {
        Name = name,
        Address = address
      };

      this.data.Add(school);

      await this.data.SaveChangesAsync();
    }
  }
}