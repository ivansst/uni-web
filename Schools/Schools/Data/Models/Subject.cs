using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schools.Data.Models
{
  public class Subject
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "NVARCHAR(2000)")]
    public string Name { get; set; }

    [Required]
    public int SchoolId { get; set; }

    public School School { get; set; }

    public virtual IList<User> Teacher { get; set; }

    public virtual IList<Class> Class { get; set; }
  }
}