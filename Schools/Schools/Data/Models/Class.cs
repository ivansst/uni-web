using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
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

    public virtual School School { get; set; }

    public virtual IList<Subject> Subject { get; set; }
  }
}