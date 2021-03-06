using System.ComponentModel.DataAnnotations;

namespace Schools.Models.UserModels
{
  public class LoginModel
  {
    [Required(ErrorMessage = "This field is required!")]
    public string Email { get; set; }

    [Required(ErrorMessage = "This field is required!")]
    public string Password { get; set; }
  }
}
