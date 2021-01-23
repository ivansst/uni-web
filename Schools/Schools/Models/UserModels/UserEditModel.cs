using System.ComponentModel.DataAnnotations;

namespace Schools.Models.UserModels
{
  public class UserEditModel
  {
    [Required(ErrorMessage = "This field is required.")]
    public string UserId { get; set; }

    public string FirstName { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }
  }
}
