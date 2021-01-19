using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Schools.Data;
using Schools.Data.Models;
using Schools.Models.UserModels.UserRequestModel;
using Schools.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Schools.Services
{
  public class UserService : IUserService
  {
    private readonly ApplicationDbContext data;
    private readonly UserManager<User> userManager;

    public UserService(ApplicationDbContext data,
                       UserManager<User> userManager) 
    { 
      this.data = data;
      this.userManager = userManager;
    }

    public async Task Create(UserCreateRequestModel model)
    {
      var user = new User
      {
        UserName = model.UserName,
        Email = model.Email,
        FirstName = model.FirstName,
        MiddleName = model.MiddleName,
        LastName = model.LastName,
        RoleId = model.RoleId,
        SchoolId = model.SchoolId
      };

      var result = await this.userManager.CreateAsync(user, model.Password);

      if(!result.Succeeded)
      {
        throw new Exception("Unnable to create user!");
      }
    }

    public async Task UpdatePersonalData(string userId, string firstName, string middleName, string lastName)
    {
      var user = await this.data.Users.FirstOrDefaultAsync(u => u.Id == userId);
      user.FirstName = firstName;
      user.MiddleName = middleName;
      user.LastName = lastName;
    }

    public async Task UpdateUserSchool(string userId, int schoolId)
    {
      var user = await this.data.Users.FirstOrDefaultAsync(u => u.Id == userId);
      user.SchoolId = schoolId;

      await this.data.SaveChangesAsync();
    }
  }
}
