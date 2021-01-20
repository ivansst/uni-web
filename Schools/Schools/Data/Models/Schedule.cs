using System;
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

    public virtual Subject Subjcet { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    [Required]
    public int Day { get; set; }
  }
}
