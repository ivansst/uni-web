using System.ComponentModel.DataAnnotations;

namespace Schools.Models.SubjectModels
{
  public class SubjectCreateRequestModel
  {
    [Required]
    public string Name { get; set; }

    public int? SchoolId { get; set; }
  }
}
