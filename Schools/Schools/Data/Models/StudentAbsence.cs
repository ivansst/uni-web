using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schools.Data.Models
{
  public class StudentAbsence
  {
    [Key]
    public int Id { get; set; }

    [Required]
    public string StudentId { get; set; }

    [ForeignKey("StudentId")]
    public User User { get; set; }

    [Required]
    public int Absences { get; set; }
  }
}
