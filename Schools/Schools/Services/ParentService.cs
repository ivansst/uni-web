using Microsoft.EntityFrameworkCore;
using Schools.Data;
using Schools.Data.Models;
using Schools.Models.UserModels;
using Schools.Services.Interfaces;
using Schools.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schools.Services
{
  public class ParentService : BaseService, IParentService
  {
    private readonly ApplicationDbContext data;

    public ParentService(ApplicationDbContext data)
    {
      this.data = data;
    }

    public async Task EditParentStudents(string userId, IEnumerable<User> students)
    {
      var parentStudents = await this.data.ParentStudents.Where(ps => ps.ParentId == userId).ToListAsync();

      this.data.ParentStudents.RemoveRange(parentStudents);

      var parentStudentModel = new ParentStudents();

      var parentStudentsList = new List<ParentStudents>();

      foreach (var student in students)
      {
        parentStudentModel = new ParentStudents
        {
          ParentId = userId,
          StudentId = student.Id
        };

        parentStudentsList.Add(parentStudentModel);
      }

      this.data.ParentStudents.AddRange(parentStudentsList);

      await this.data.SaveChangesAsync();
    }

    public async Task<ParentEditViewModel> GetEditViewModel(string userId)
    {
      var parent = await this.data.Users.FirstOrDefaultAsync(u => u.Id == userId);

      if (parent == null)
      {
        throw new Exception("User doesn't exist");
      }

      var parentStudentsIds = await this.data.ParentStudents.Where(ps => ps.ParentId == parent.Id).Select(ps => ps.StudentId).ToListAsync();

      var students = await this.data.Users.Where(u => parentStudentsIds.Contains(u.Id)).ToListAsync(); 

      var studentsForSchool = await this.data.Users.Where(u => u.SchoolId == parent.SchoolId && u.Role == "Ученик").ToListAsync();

      var userEditModel = new UserEditModel
      {
        UserId = parent.Id,
        FirstName = parent.FirstName,
        MiddleName = parent.MiddleName,
        LastName = parent.LastName
      };

      var model = new ParentEditViewModel
      {
        UserEditModel = userEditModel,
        ParentStudents = students,
        AllStudents = studentsForSchool
      };

      return model;
    }
  }
}
