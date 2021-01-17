using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schools.Data.Models
{
  public class Subject
  {
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "NVARCHAR(2000)")]
    public string Name { get; set; }

    public int SchoolId { get; set; }

    public School School { get; set; }
  }
}
