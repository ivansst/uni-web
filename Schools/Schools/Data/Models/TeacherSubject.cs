using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schools.Data.Models
{
  public class TeacherSubject
  {
    [Key]
    public int Id { get; set; }

    [Required]
    public string TeacherId { get; set; }

    [ForeignKey("TeacherId")]
    public User User { get; set; }

    [Required]
    public int SubjectId { get; set; }

    public Subject Subject { get; set; }
  }
}