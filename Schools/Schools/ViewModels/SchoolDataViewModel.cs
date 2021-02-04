using Schools.Models.SchoolModels;

namespace Schools.ViewModels
{
  public class SchoolDataViewModel
  {
    public SchoolModel School { get; set; }

    public SchoolPrincipalModel Principal { get; set; }

    public SchoolStatisticModel Statistics { get; set; }

  }
}
