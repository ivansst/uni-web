using Schools.ViewModels;
using System.Threading.Tasks;

namespace Schools.Services.Interfaces
{
  public interface IStudentService
  {
    Task<StudentEditViewModel> GetViewModel(string userId);

    Task SaveStudentClass(string studentId, int classId);
  }
}
