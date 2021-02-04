using Schools.Models.SchoolModels;
using Schools.Models.SubjectModels;
using System.Collections.Generic;

namespace Schools.ViewModels
{
  public class ScheduleCreateViewModel
  {
    public IEnumerable<SchoolScheduleModel> ScheduleModels;
    public IEnumerable<SubjectModel> Subjects { get; set; } = new List<SubjectModel>();
  }
}
