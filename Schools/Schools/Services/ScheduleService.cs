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
      if (school == null)
      {
        throw new Exception("Can't create schedule for non existant school");
      }

      var schedules = new List<Schedule>();

      foreach (var schedule in scheduleModels)
      {
        schedules.Add(new Schedule
        {
          Day = schedule.Day,
          SchoolId = schoolId,
          SubjectId = schedule.SubjectId,
          Order = schedule.Order
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

      var result = schedules.Select(s => new ScheduleModel
      {
        Order = s.Order,
        Day = s.Day,
        SubjectName = s.Subject.Name
      });

      return result;
    }

    public async Task<ScheduleEditViewModel> GetScheduleEditModel(int schoolId)
    {
      var schoolSubjects = await this.data.Subjects.Where(s => s.SchoolId == schoolId)
                                                   .Select(s => new SubjectModel
                                                   {
                                                     Id = s.Id,
                                                     Name = s.Name,
                                                     SchoolId = s.Id
                                                   })
                                                    .ToListAsync();

      var schedules = await this.data.Schedules.Include(s => s.Subject)
                                               .Where(s => s.SchoolId == schoolId)
                                               .ToListAsync();

      var scheduleModels = schedules.Select(s => new ScheduleEditModel
      {
        Day = s.Day,
        SubjectId = s.Subject.Id,
        SubjectName = s.Subject.Name,
        Order = s.Order
      });

      var model = new ScheduleEditViewModel
      {
        ScheduleEditModels = scheduleModels,
        Subjects = schoolSubjects
      };

      return model;
    }

    public async Task EditSchedule(int schoolId, IEnumerable<ScheduleEditModel> scheduleEditModels)
    {
      var currentSchedules = await this.data.Schedules.Where(s => s.SchoolId == schoolId).ToListAsync();

      this.data.Schedules.RemoveRange(currentSchedules);

      var schedules = scheduleEditModels.Select(sm => new Schedule
      {
        Day = sm.Day,
        SubjectId = sm.SubjectId,
        Order = sm.Order,
        SchoolId = schoolId
      });

      this.data.Schedules.AddRange(schedules);

      await this.data.SaveChangesAsync();
    }
  }
}
