using Microsoft.EntityFrameworkCore;
using Schools.Data;
using Schools.Data.Models;
using Schools.Models.ParentModels;
using Schools.Models.UserModels;
using Schools.Services.Interfaces;
using Schools.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schools.Services
{
  public class ParentService : IParentService
  {
    private readonly ApplicationDbContext data;

    public ParentService(ApplicationDbContext data)
    {
      this.data = data;
    }

    public async Task EditParentStudents(string userId, IEnumerable<string> students)
    {
      var parentStudents = await this.data.ParentStudents
                                                  .Where(ps => ps.ParentId == userId)
                                                  .ToListAsync();

      this.data.ParentStudents.RemoveRange(parentStudents);

      var parentStudentModel = new ParentStudents();
      var parentStudentsList = new List<ParentStudents>();

      foreach (var student in students)
      {
        parentStudentModel = new ParentStudents
        {
          ParentId = userId,
          StudentId = student
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

      var studentsForSchool = await this.data.Users.Where(u => u.SchoolId == parent.SchoolId && u.Role == "Student").ToListAsync();

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

    public async Task<IEnumerable<User>> GetAll(int schoolId)
    {
      var parents = await this.data.Users.Where(c => c.SchoolId == schoolId && c.Role == "Parent").ToListAsync();

      if(parents == null)
      {
        throw new Exception("There is no school with this id!");
      }

      return parents;
    }

    public async Task<IEnumerable<User>> GetParentStudents(string parentId)
    {
      var parentStudentsIds = await this.data.ParentStudents.Where(ps => ps.ParentId == parentId).Select(ps => ps.StudentId).ToListAsync();

      var students = await this.data.Users.Where(u => parentStudentsIds.Contains(u.Id)).ToListAsync();

      if (students == null)
      {
        students = new List<User>();
      }

      return students;
    }

    public async Task<ParentStudentsViewModel> GetParentStudentsViewModel(string parentId)
    {
      var parentStudentsIds = await this.data.ParentStudents.Where(ps => ps.ParentId == parentId).Select(ps => ps.StudentId).ToListAsync();

      var students = await this.data.Users.Where(u => parentStudentsIds.Contains(u.Id)).ToListAsync();

      var schools = await this.data.Schools.ToListAsync();
      var schoolsIds = schools.Select(s => s.Id).ToList();

      var classes = await this.data.Classes.Where(c => schoolsIds.Contains(c.SchoolId)).ToListAsync();
      var classesIds = classes.Select(c => c.Id).ToList();

      var studentClasses = await this.data.StudentClass.Include(sc => sc.Class).Where(sc => classesIds.Contains(sc.ClassId)).ToListAsync();

      var parentStudents = new List<ParentStudentsModel>();

      var schoolName = string.Empty;
      var classData = new Class { };
      var className = string.Empty;

      foreach(var student in students)
      {
        schoolName = schools.FirstOrDefault(s => s.Id == student.SchoolId).Name;

        classData = studentClasses.FirstOrDefault(sc => sc.StudentId == student.Id).Class;
        className = $"{classData.Name} {classData.Group}";

        parentStudents.Add(new ParentStudentsModel
        {
          UserId = student.Id,
          FirstName = student.FirstName,
          MiddleName = student.MiddleName,
          LastName = student.LastName,
          Email = student.Email,
          SchoolName = schoolName,
          Class = className
        });
      }

      var model = new ParentStudentsViewModel
      {
        ParentStudents = parentStudents,
      };

      return model;
    }

  }
}
