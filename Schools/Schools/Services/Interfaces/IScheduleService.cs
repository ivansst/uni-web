using Schools.ViewModels;
using System.Threading.Tasks;

namespace Schools.Services.Interfaces
{
  public interface IScheduleService
  {
    Task<ScheduleCreateViewModel> GetCreateViewModel(int schoolId);
  }
}
