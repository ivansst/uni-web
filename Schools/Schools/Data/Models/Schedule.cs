using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    [Required]
    public int Day { get; set; }

    public string TeacherId { get; set; }

    [ForeignKey("TeacherId")]
    public virtual User Teacher { get; set; }

    public int ClassId { get; set; }

    public Class Class { get; set; }
  }
}
