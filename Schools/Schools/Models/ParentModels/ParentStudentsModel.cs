using Schools.Models.UserModels;

namespace Schools.Models.ParentModels
{
  public class ParentStudentsModel : UserEditModel
  {
    public string Email { get; set; }

    public string SchoolName { get; set; }

    public string Class { get; set; }
  }
}
