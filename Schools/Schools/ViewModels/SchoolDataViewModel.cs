using Schools.Data.Models;
using Schools.Models.SchoolModels;

namespace Schools.ViewModels
{
  public class SchoolDataViewModel
  {
    public School School { get; set; }

    public SchoolPrincipalModel Principal { get; set; }

    public SchoolStatisticModel Statistics { get; set; }

  }
}
