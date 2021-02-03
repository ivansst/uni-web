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

    Task<IEnumerable<SubjectModel>> GetAll(int schoolId);

    Task<IEnumerable<SubjectModel>> GetSubjectsForClassAndTeacher(string userId, int classId);

    Task<IEnumerable<SubjectModel>> GetSubjectsForTeacher(string userId);
  }
}
