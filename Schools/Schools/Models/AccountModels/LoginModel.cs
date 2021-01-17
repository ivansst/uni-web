using System.ComponentModel.DataAnnotations;

namespace Schools.Models.AccountModels
{
  public class LoginModel
  {
    [Required(ErrorMessage = "This field is required!")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "This field is required!")]
    public string Password { get; set; }
  }
}
