using Microsoft.EntityFrameworkCore;
using Schools.Data;
using Schools.Models.SchoolModels;
using Schools.Models.SubjectModels;
using Schools.Services.Interfaces;
using Schools.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schools.Services
{
  public class ScheduleService : IScheduleService
  {
    private readonly ApplicationDbContext data;

    public ScheduleService(ApplicationDbContext data)
    {
      this.data = data;
    }

    public async Task<ScheduleCreateViewModel> GetCreateViewModel(int schoolId)
    {
      var schoolSubjects = await this.data.Subjects.Where(s => s.SchoolId == schoolId)
                                                   .Select(s => new SubjectModel
                                                   {
                                                     Id = s.Id,
                                                     Name = s.Name,
                                                     SchoolId = s.Id
                                                   })
                                                    .ToListAsync();

      var scheduleModels = new List<SchoolScheduleModel>();

      for (int day = 1; day < 6; day++)
      {
        for (int order = 1; order < 7; order++)
        {
          scheduleModels.Add(new SchoolScheduleModel
          {
            Day = day,
            Order = order
          });
        }
      }

      var resultModel = new ScheduleCreateViewModel
      {
        ScheduleModels = scheduleModels,
        Subjects = schoolSubjects
      };

      return resultModel;
    }
  }
}
