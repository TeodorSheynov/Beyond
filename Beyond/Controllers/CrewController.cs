using Beyond.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace Beyond.Controllers
{
    public class CrewController : Controller
    {
        private readonly ITakeModels _takeViewModels;
        public CrewController(ITakeModels takeViewModels)
        {
            _takeViewModels = takeViewModels;
        }

        [Route("/Crew")]
        [Authorize]
        public IActionResult Crew()
        {
            var crew = _takeViewModels.CrewOrNull();
            switch (crew)
            {
                case null:
                    ViewData["Message"]="There are no crew members.";
                    return View("Error");
                default:
                    return View(crew);
            }
        }
    }
}
