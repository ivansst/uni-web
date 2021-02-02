using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Schools.Models.ClassModels
{
  public class ClassEditRequestModel
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "This field is required!")]
    public int Name { get; set; }

    [Required(ErrorMessage = "This field is required!")]
    public string Group { get; set; }

    public IEnumerable<ClassSubjectEditModel> AllSubjects { get; set; }

    public IEnumerable<int> SubjectIds { get; set; } = new List<int>();
  }
}
