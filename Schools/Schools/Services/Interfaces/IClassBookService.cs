using Schools.Models.ClassBookModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schools.Services.Interfaces
{
  public interface IClassBookService
  {
    Task AddGrade(string userId, int subjectId, int value);

    Task AddAbsence(string userId, int value);

    Task<IEnumerable<ClassBookModel>> GetViewModel(int classId, int subjectId);

  }
}
