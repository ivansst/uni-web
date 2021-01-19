using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Schools.Data.Models
{
  public class User : IdentityUser
  {
    public string FirstName { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }

    [Required]
    public string RoleId { get; set; }

    public IdentityRole Role { get; set; }

    [Required]
    public int SchoolId { get; set; }

    public School School { get; set; }

    public DateTime CreationTime { get; set; }
  }
}
