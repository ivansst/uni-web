using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schools.Models.ScheduleModels
{
  public class ScheduleModel
  {
    public int Order { get; set; }

    public int Day { get; set; }

    public string Subject { get; set; }
  }
}
