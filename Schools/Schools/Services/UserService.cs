using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Schools.Data;
using Schools.Data.Models;
using Schools.Models.UserModels;
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

    public async Task<UserEditModel> GetEditViewModel(string userId)
    {
      var user = await this.data.Users.FirstOrDefaultAsync(u => u.Id == userId);

      var model = new UserEditModel
      {
        FirstName = user.FirstName,
        MiddleName = user.MiddleName,
        LastName = user.LastName,
        UserId = user.Id,
      };

      return model;
    }

    public async Task<User> Create(UserCreateRequestModel model)
    {
      var user = new User
      {
        UserName = model.UserName,
        Email = model.Email,
        FirstName = model.FirstName,
        MiddleName = model.MiddleName,
        LastName = model.LastName,
        Role = model.Role,
        SchoolId = model.SchoolId
      };

      var result = await this.userManager.CreateAsync(user, model.Password);

      if(!result.Succeeded)
      {
        throw new Exception("Unnable to create user!");
      }

      return user;
    }

    public async Task<int> GetSchoolIdForUser(string userId)
    {
      var schoolId = (await this.data.Users.FirstOrDefaultAsync(u => u.Id == userId)).SchoolId;

      if (!schoolId.HasValue)
      {
        throw new Exception("User is not registered in school");
      }

      return schoolId.Value;
    }

    public async Task UpdatePersonalData(UserEditModel model)
    {
      var user = await this.data.Users.FirstOrDefaultAsync(u => u.Id == model.UserId);


      if (user.FirstName == model.FirstName &&
         user.MiddleName == model.MiddleName &&
         user.LastName == model.LastName)
      {
        return;
      }

      user.FirstName = model.FirstName;
      user.MiddleName = model.MiddleName;
      user.LastName = model.LastName;

      await this.data.SaveChangesAsync();
    }

    public async Task UpdateUserSchool(string userId, int? schoolId = null)
    {
      var user = await this.data.Users.FirstOrDefaultAsync(u => u.Id == userId);
      user.SchoolId = schoolId;

      await this.data.SaveChangesAsync();
    }
  }
}
