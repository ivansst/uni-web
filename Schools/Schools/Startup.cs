using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Schools.Extensions;

namespace Schools
{
  public class Startup
  {
    public Startup(IConfiguration configuration) => this.Configuration = configuration;

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDatabase(this.Configuration)
              .AddIdentity()
              .AddCookieAuthentication();


      services.AddControllersWithViews();
      services.AddRazorPages();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.ApplyMigrations();
      }

      app.UseHttpsRedirection()
         .UseStaticFiles()
         .UseRouting()
         .UseCookiePolicy();

      app.UseAuthentication()
         .UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Account}/{action=Login}");
        endpoints.MapRazorPages();
      }); 
    }
  }
}
