using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schools.Data.Models
{
  public class ParentStudents
  {
    [Key]
    public int Id { get; set; }

    public string ParentId { get; set; }

    [ForeignKey("ParentId")]
    public virtual User Parent { get; set; }

    [Required]
    public string StudentId { get; set; }

    [ForeignKey("StudentId")]
    public virtual User Student { get; set; }
  }
}
