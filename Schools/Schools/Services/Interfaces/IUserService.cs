using Schools.Models.UserModels.UserRequestModel;
using System.Threading.Tasks;

namespace Schools.Services.Interfaces
{
  public interface IUserService
  {
    Task Create(UserCreateRequestModel model);

    Task UpdateUserSchool(string userId, int schoolId);

    Task UpdatePersonalData(string userId, string firstName, string middleName, string lastName);
  }
}
