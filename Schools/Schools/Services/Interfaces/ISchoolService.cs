using Schools.Data.Models;
using Schools.Models.SchoolModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schools.Services.Interfaces
{
  public interface ISchoolService
  {
    Task<IEnumerable<School>> GetAll();

    Task Edit(SaveSchoolRequestModel model);

    Task Create(SaveSchoolRequestModel model);

    Task AssignNewPrincipal(int schoolId, string userId);

    Task RemoveUserFromSchool(int schoolId, string userId);

    Task<SchoolPrincipalModel> GetPrincipal(int schoolId);

    Task<School> GetSchoolData(int schoolId);
  }
}
