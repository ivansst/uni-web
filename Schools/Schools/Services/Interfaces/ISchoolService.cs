using Schools.Data.Models;
using Schools.Models.SchoolModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schools.Services.Interfaces
{
  public interface ISchoolService
  {
    Task<IEnumerable<School>> GetAll();

    Task Save(int id, string name, string address);

    Task AssignNewPrincipal(int schoolId, string userId);

    Task RemoveUserFromSchool(int schoolId, string userId);

    Task<SchoolPrincipalModel> GetPrincipal(int schoolId);
  }
}
