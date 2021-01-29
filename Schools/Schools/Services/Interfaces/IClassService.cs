using Schools.Data.Models;
using Schools.Models.ClassModels;
using Schools.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schools.Services.Interfaces
{
  public interface IClassService
  {
    Task Save(ClassSaveRequestModel model);

    Task AddStudentToClass(string studentId, int classId);

    Task RemoveStudentFromClass(string studentId, int classId);

    Task<ClassSaveViewModel> GetSaveViewModel(int? classId);

    Task<List<Class>> GetAll(int? schoolId);
  }
}
