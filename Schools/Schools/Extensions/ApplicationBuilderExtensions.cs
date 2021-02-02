using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Schools.Data;

namespace Schools.Extensions
{
  public static class ApplicationBuilderExtensions
  {
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
      using var services = app.ApplicationServices.CreateScope();

      var dbContext = services.ServiceProvider.GetService<ApplicationDbContext>();

      dbContext.Database.Migrate();

      Seeder.Seed(dbContext);
    }
  }
}
