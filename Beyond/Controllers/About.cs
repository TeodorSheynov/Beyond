using Microsoft.AspNetCore.Mvc;

namespace Beyond.Controllers
{
    public class About : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}