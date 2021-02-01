using Schools.Data.Models;
using Schools.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schools.Services.Interfaces
{
  public interface IStudentService
  {
    Task<StudentEditViewModel> GetViewModel(string userId);

    Task SaveStudentClass(string studentId, int classId);

    Task<IEnumerable<User>> GetAll(int schoolId);
  }
}
