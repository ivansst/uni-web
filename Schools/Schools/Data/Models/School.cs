using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schools.Data.Models
{
  public class School
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "NVARCHAR(1000)")]
    public string Name { get; set; }

    [Required]
    [Column(TypeName = "NVARCHAR(3000)")]
    public string Address { get; set; }
  }
}
