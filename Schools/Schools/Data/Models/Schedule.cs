using System.ComponentModel.DataAnnotations;

namespace Schools.Data.Models
{
  public class Schedule
  {
    [Key]
    public int Id { get; set; }

    [Required]
    public int SchoolId { get; set; }

    public virtual School School { get; set; }

    [Required]
    public int SubjectId { get; set; }

    public virtual Subject Subject { get; set; }

    public int Order { get; set; }

    [Required]
    public int Day { get; set; }
  }
}