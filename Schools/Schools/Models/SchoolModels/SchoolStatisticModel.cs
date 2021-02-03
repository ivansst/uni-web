using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schools.Models.SchoolModels
{
  public class SchoolStatisticModel
  {
    public int TeacherCount { get; set; }

    public int StudentCount { get; set; }

    public int ParentCount { get; set; }

    public int ClassesCount { get; set; }

    public int SubjectsCount { get; set; }

    public int Absences { get; set; }

    public double GradeAverage { get; set; }

  }
}
