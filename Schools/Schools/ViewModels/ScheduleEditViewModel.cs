using Schools.Models.ScheduleModels;
using Schools.Models.SubjectModels;
using System.Collections.Generic;

namespace Schools.ViewModels
{
  public class ScheduleEditViewModel
  {

    public IEnumerable<ScheduleEditModel> ScheduleEditModels { get; set; }

    public IEnumerable<int> SubjectIds { get; set; } = new List<int>();

    public IEnumerable<SubjectModel> Subjects { get; set; } = new List<SubjectModel>();
  }
}
