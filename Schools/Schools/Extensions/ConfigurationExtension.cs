using Microsoft.Extensions.Configuration;

namespace Schools.Extensions
{
  public static class ConfigurationExtension
  {
    public static string GetDefaultConnectionString(this IConfiguration configuration)
       => configuration.GetConnectionString("DefaultConnection");
  }
}
