using Schools.Data.Models;
using Schools.Models.ClassModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schools.Services.Interfaces
{
  public interface IClassService
  {
    Task Edit(ClassEditRequestModel model);

    Task Create(ClassCreateRequestModel model);

    Task AddStudentToClass(string studentId, int classId);

    Task RemoveStudentFromClass(string studentId, int classId);

    Task<ClassEditRequestModel> GetEditModel(int classId);

    Task<IEnumerable<Class>> GetAll(int schoolId);
  }
}
