using Schools.Data.Models;
using Schools.Models.SubjectModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schools.Services.Interfaces
{
  public interface ISubjectService
  {
    Task Create(SubjectCreateRequestModel model);

    Task Delete(int Id);

    Task<List<Subject>> GetAll(int? schoolId);
  }
}
