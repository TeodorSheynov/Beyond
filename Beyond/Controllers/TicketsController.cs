using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Beyond.Controllers
{
    public class TicketsController : Controller
    {
        [Authorize]
        public IActionResult All()
        {
            return View();
        }
        [Authorize]
        public IActionResult More(string id)
        {
            ViewData["id"]=id;
            return View();
        }
    }
}
