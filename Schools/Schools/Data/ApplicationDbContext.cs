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

    public DbSet<Subject> Subjects { get; set; }

    public DbSet<Schedule> Schedules { get; set; }

    public DbSet<StudentAbsence> StudentAbsences { get; set; }

    public DbSet<StudentGrade> StudentGrades { get; set; }

    public DbSet<StudentClass> StudentClass { get; set; }

    public DbSet<ParentStudents> ParentStudents { get; set; }

    public DbSet<Class> Classes { get; set; }

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
        .WithMany()
        .OnDelete(DeleteBehavior.ClientCascade);

      builder.Entity<User>()
        .Property(u => u.CreationTime)
        .HasDefaultValueSql("GETUTCDATE()");

      builder.Entity<User>()
        .HasMany(s => s.Subject)
        .WithMany(s => s.Teacher)
        .UsingEntity(join => join.ToTable("TeacherSubject"));

      base.OnModelCreating(builder);
    }
  }
}
