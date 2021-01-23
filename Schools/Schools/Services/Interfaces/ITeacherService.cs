using Schools.Data.Models;
using Schools.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schools.Services.Interfaces
{
  public interface ITeacherService
  {
    Task<TeacherEditViewModel> GetTeacherEditViewModel(string teacherId);

    Task UpdateClassSubjects(string teacherId, List<Subject> subjects);
  }
}
