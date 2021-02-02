using Microsoft.AspNetCore.Identity;
using Schools.Data.Models;
using System.Linq;

namespace Schools.Data
{
  public static class Seeder
  {
    public static void Seed(this ApplicationDbContext data)
    {
      if (data.Users.Any())
      {
        return;
      }

      var password = "administratorPassword@Project";
      var email = "administrator@schools.university";
      var userName = "administrator";

      var user = new User
      {
        FirstName = "Administrator",
        MiddleName = "At",
        LastName = "Schools",
        UserName = userName,
        NormalizedEmail = email.ToUpper(),
        Email = email,
        NormalizedUserName = userName.ToUpper(),
        Role="Administrator"
      };

      var passwordHasher = new PasswordHasher<User>();
      var hashedPassword = passwordHasher.HashPassword(user, password);
      user.PasswordHash = hashedPassword;

      data.Users.Add(user);

      data.SaveChanges();
    }
  }
}
