using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schools.Data.Models
{
  public class StudentSubject
  {
    [Key]
    public int Id { get; set; }

    [Required]
    public string StudentId { get; set; }

    [ForeignKey("StudentId")]
    public User User { get; set; }

    [Required]
    public int SubjectId { get; set; }

    public Subject Subject { get; set; }
  }
}
