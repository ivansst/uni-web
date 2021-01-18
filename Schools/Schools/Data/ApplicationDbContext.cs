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

    public DbSet<School> Schools { get; set; }

    public DbSet<Subject> Subjects {get;set;}

    public DbSet<StudentSubject> StudentSubjects { get; set; }

    public DbSet<TeacherSubject> TeacherSubjects { get; set; }

    public DbSet<Schedule> Schedules { get; set; }

    public DbSet<StudentAbsence> StudentAbsences { get; set; }

    public DbSet<StudentGrade> StudentGrades { get; set; }

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

      builder.Entity<Subject>()
        .HasOne(s => s.School)
        .WithOne()
        .OnDelete(DeleteBehavior.ClientCascade);

      builder.Entity<User>()
        .Property(u => u.CreationTime)
        .HasDefaultValueSql("GETUTCDATE()");

      base.OnModelCreating(builder);
    }
  }
}
