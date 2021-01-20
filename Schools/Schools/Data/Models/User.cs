using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Schools.Data.Models
{
  public class User : IdentityUser
  {
    public string FirstName { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }
  
    public string Role { get; set; }

    [Required]
    public int SchoolId { get; set; }

    public virtual School School { get; set; }

    public DateTime CreationTime { get; set; }

    public virtual IList<Subject> Subject { get; set; }
  }
}
