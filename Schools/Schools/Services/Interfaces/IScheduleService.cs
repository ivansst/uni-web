using Schools.Models.ScheduleModels;
using Schools.Models.SchoolModels;
using Schools.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schools.Services.Interfaces
{
  public interface IScheduleService
  {
    Task<IEnumerable<ScheduleModel>> GetSchedule(int schoolId);

    Task<ScheduleCreateViewModel> GetCreateViewModel(int schoolId);

    Task Create(int schoolId, IEnumerable<ScheduleCreateModel> scheduleCreateModels);

    Task<ScheduleEditViewModel> GetScheduleEditModel(int schoolId);

    Task EditSchedule(int schoolId, IEnumerable<ScheduleEditModel> scheduleEditModels);
  }
}
