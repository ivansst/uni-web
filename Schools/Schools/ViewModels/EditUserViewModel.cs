using Schools.Models.UserModels;
using System.ComponentModel.DataAnnotations;

namespace Schools.ViewModels
{
  public class EditUserViewModel : UserEditModel
  {
    public string Email { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "This field is required!")]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "This field is required!")]
    [Compare(nameof(NewPassword), ErrorMessage = "Passwords must match!")]
    public string ConfirmNewPassword { get; set; }
  }
}
