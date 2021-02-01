﻿using Schools.Data.Models;
using Schools.Models.UserModels;
using Schools.ViewModels;
using System.Threading.Tasks;

namespace Schools.Services.Interfaces
{
  public interface IUserService
  {
    Task<UserEditModel> GetEditViewModel(string userName);

    Task Create(UserCreateRequestModel model);

    Task UpdateUserSchool(string userId, int schoolId);

    Task UpdatePersonalData(UserEditModel model);

    Task<int> GetSchoolIdForUser(string userName);
  }
}