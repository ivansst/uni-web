using Schools.Models.ClassBookModels;
using System.Collections.Generic;

namespace Schools.ViewModels
{
  public class ClassBookViewModel
  {
    public IEnumerable<ClassBookModel> ClassBookModels { get; set; }

    public AddGradeModel AddGradeModel { get; set; } = new AddGradeModel { };

    public AddAbsenceModel AddAbsenceModel { get; set; } = new AddAbsenceModel { };
    public int SubjectId { get; set; }

    public int ClassId { get; set; }
  }
}
