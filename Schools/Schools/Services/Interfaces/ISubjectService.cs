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

    Task<IEnumerable<Subject>> GetAll(int schoolId);

    Task<IEnumerable<Subject>> GetSubjectsForClassAndTeacher(string userId, int classId);

    Task<IEnumerable<Subject>> GetSubjectsForTeacher(string userId);
  }
}
