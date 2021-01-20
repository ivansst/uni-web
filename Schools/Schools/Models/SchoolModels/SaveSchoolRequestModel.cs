using System.ComponentModel.DataAnnotations;

namespace Schools.Models.SchoolModels
{
  public class SaveSchoolRequestModel
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "This field is required!")]
    public string Name { get; set; }

    [Required(ErrorMessage = "This field is required!")]
    public string Address { get; set; }
  }
}
