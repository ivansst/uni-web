using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Schools.Data.Models;

namespace Schools.Data
{
  public class ApplicationDbContext : IdentityDbContext<User>
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<User>()
        .Ignore(u => u.EmailConfirmed)
        .Ignore(u => u.AccessFailedCount)
        .Ignore(u => u.LockoutEnabled)
        .Ignore(u => u.SecurityStamp)
        .Ignore(u => u.ConcurrencyStamp)
        .Ignore(u => u.PhoneNumber)
        .Ignore(u => u.PhoneNumberConfirmed)
        .Ignore(u => u.TwoFactorEnabled)
        .Ignore(u => u.LockoutEnd)
        .Ignore(u => u.ConcurrencyStamp);

      base.OnModelCreating(builder);
    }
  }
}
