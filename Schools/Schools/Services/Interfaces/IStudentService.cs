using Schools.ViewModels;
using System.Threading.Tasks;

namespace Schools.Services.Interfaces
{
  public interface IStudentService
  {
    Task EditStudent(StudentEditViewModel model);
  }
}
