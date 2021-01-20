using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schools.Data.Models
{
  public class StudentClass
  {
    [Required]
    public string StudentId { get; set; }

    [ForeignKey("StudentId")]
    public virtual User Student { get; set; }

    [Required]
    public int ClassId { get; set; }

    public virtual Class Class { get; set; }
  }
}
