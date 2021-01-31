using Microsoft.AspNetCore.Mvc;
using Schools.Services.Interfaces;
using System.Threading.Tasks;

namespace Schools.Controllers
{
  public class BaseController : Controller
  {
    protected string UserName => User.Identity.Name;
  }
}
