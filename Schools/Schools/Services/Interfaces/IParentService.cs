using Schools.Data.Models;
using Schools.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schools.Services.Interfaces
{
  public interface IParentService
  {
    Task<ParentEditViewModel> GetEditViewModel(string userId);

    Task EditParentStudents(string userId, IEnumerable<string> students);

    Task<IEnumerable<User>> GetAll(int schoolId);

    Task<IEnumerable<User>> GetParentStudents(string parentId);

    Task<ParentStudentsViewModel> GetParentStudentsViewModel(string parentId);
  }
}
