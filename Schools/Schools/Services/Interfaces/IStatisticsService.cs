using Schools.Models.SchoolModels;

namespace Schools.Services.Interfaces
{
  public interface IStatisticsService
  {
    SchoolStatisticModel GetStatistics(int schoolId);

  }
}
