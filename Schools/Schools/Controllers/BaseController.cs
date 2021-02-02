using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace Schools.Controllers
{
  public class BaseController : Controller
  {
    protected string UserId => User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
  }
}
