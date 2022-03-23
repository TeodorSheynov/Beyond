using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Beyond.Controllers
{
    public class DestinationsController : Controller
    {
        [Authorize]
        [Route("/Destinations")]
        public IActionResult Destinations() => View();
    }
}
