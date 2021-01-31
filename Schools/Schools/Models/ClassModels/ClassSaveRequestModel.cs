using Schools.Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Schools.Models.ClassModels
{
  public class ClassSaveRequestModel
  {
    public int? Id { get; set; }

    [Required(ErrorMessage = "This field is required!")]
    public int Name { get; set; }

    [Required(ErrorMessage = "This field is required!")]
    public string Group { get; set; }

    public int SchoolId { get; set; }

    public IEnumerable<Subject> Subjects { get; set; }
  }
}
