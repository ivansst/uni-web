using Microsoft.AspNetCore.Identity;

namespace Schools.Data.Models
{
  public class User : IdentityUser
  {
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Role { get; set; }

    public int SchoolId { get; set; }

    public School School { get; set; }
  }
}
