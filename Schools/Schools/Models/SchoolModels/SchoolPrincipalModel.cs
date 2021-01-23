using Schools.Models.UserModels;

namespace Schools.Models.SchoolModels
{
  public class SchoolPrincipalModel
  {
    public UserEditModel UserEditModel { get; set; }

    public string Role { get; set; }

    public string SchoolName { get; set; }
  }
}
