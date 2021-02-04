using Microsoft.EntityFrameworkCore;
using Schools.Data;
using Schools.Data.Models;
using Schools.Models.ScheduleModels;
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

    public async Task Create(int schoolId, IEnumerable<ScheduleCreateModel> scheduleModels)
    {
      var school = await this.data.Schools.FirstOrDefaultAsync(s => s.Id == schoolId);
      if(school == null)
      {
        throw new Exception("Can't create schedule for non existant school");
      }

      var schedules = new List<Schedule>();

      foreach (var model in scheduleModels)
      {
        schedules.Add(new Schedule
        {
          Day = model.Day,
          SchoolId = schoolId,
          SubjectId = model.SubjectId,
          Order = model.Order
        });
      }

      this.data.Schedules.AddRange(schedules);

      await this.data.SaveChangesAsync();
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

      var resultModel = new ScheduleCreateViewModel
      {
        Subjects = schoolSubjects
      };

      return resultModel;
    }

    public async Task<IEnumerable<ScheduleModel>> GetSchedule(int schoolId)
    {
      var schedules = await this.data.Schedules.Include(s => s.Subject)
                                               .Where(s => s.SchoolId == schoolId)
                                               .ToListAsync();

      var result = new List<ScheduleModel>();

      foreach (var schedule in schedules)
      {
        result.Add(new ScheduleModel
        {
          Order = schedule.Order,
          Day =schedule.Day,
          Subject = schedule.Subject.Name
        });
      }

      return result;
    }
  }
}
