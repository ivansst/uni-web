using System.Threading.Tasks;

namespace Schools.Services.Interfaces
{
  public interface ISchoolService
  {
    Task Save(int id, string name, string address);

    Task AssignPrincipal(int schoolId, string userId);
  }
}
