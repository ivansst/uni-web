using Schools.Data.Models;
using Schools.Models.UserModels;

namespace Schools.Services
{
  public class BaseService
  {
    protected bool IsUserDataChanged(User user, UserEditModel model)
    {
      if (user.FirstName == model.FirstName ||
          user.MiddleName == model.MiddleName ||
          user.LastName == model.LastName)
      {
        return true;
      }

      return false;
    }
  }
}
