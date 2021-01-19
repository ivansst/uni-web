using System.Threading.Tasks;

namespace Schools.Services.Interfaces
{
  public interface ISchoolService
  {
    Task Create(string name, string address);
  }
}
