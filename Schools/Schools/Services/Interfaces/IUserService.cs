using Schools.Models.UserModels;
using System.Threading.Tasks;

namespace Schools.Services.Interfaces
{
  public interface IUserService
  {
    Task Create(UserCreateRequestModel model);

    Task UpdateUserSchool(string userId, int schoolId);

    Task UpdatePersonalData(UserEditModel model);
  }
}