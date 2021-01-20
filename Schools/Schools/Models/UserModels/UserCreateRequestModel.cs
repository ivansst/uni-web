namespace Schools.Models.UserModels.UserRequestModel
{
  public class UserCreateRequestModel
  {
    public string UserName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string FirstName { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }

    public int SchoolId { get; set; }

    public string Role { get; set; }
  }
}
