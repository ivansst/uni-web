using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schools.Data.Models
{
  public class ClassSubject
  {
    [Key]
    public int Id { get; set; }

    [Required]
    public string ClassId { get; set; }

    [ForeignKey("ClassId")]
    public virtual Class Class { get; set; }

    [Required]
    public string SubjectId { get; set; }

    [ForeignKey("SubjectId")]
    public virtual Subject Subject { get; set; }
  }
}
