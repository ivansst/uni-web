using Schools.Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schools.Models.SubjectModels
{
  public class SubjectCreateRequestModel
  {
    [Required]
    public string Name { get; set; }

    [Required]
    public int SchoolId { get; set; }

  }
}
