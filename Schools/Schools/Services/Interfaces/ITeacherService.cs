using Schools.Data.Models;
using Schools.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schools.Services.Interfaces
{
  public interface ITeacherService
  {
    Task<IEnumerable<User>> GetAll(int schoolId);

    Task<TeacherEditViewModel> GetTeacherEditViewModel(string teacherId);

    Task UpdateClassSubjects(string teacherId, IEnumerable<int> subjectIds);
  }
}
