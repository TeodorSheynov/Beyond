using Beyond.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace Beyond.Controllers
{
    public class CrewController : Controller
    {
        private readonly ITakeViewModels _takeViewModels;
        public CrewController(ITakeViewModels takeViewModels)
        {
            _takeViewModels = takeViewModels;
        }

        [Route("/Crew")]
        [Authorize]
        public IActionResult Crew()
        {
            var crew = _takeViewModels.CrewMembersOrNull();
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
