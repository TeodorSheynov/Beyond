using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Beyond.Controllers
{
    public class CrewController : Controller
    {

        [Route("/Crew")]
        [Authorize]
        public IActionResult Crew() => View();
    }
}
