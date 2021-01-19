using System.ComponentModel.DataAnnotations;

namespace Schools.Data.Models
{
  public class Class
  {
    [Key]
    public int Id { get; set; }

    [Required]
    public int Name { get; set; }

    [Required]
    public string Group { get; set; }

    [Required]
    public int SchoolId { get; set; }

    public School School { get; set; }
  }
}
