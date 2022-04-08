using Beyond.Data;
using Beyond.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Beyond.Controllers
{
    public class DestinationsController : Controller
    {
        private readonly ITakeViewModels _takeViewModels;
        public DestinationsController(ITakeViewModels takeViewModels)
        {
            _takeViewModels = takeViewModels;
        }
        [Authorize]
        [Route("/Destinations")]
        public IActionResult Destinations()
        {
            var destinations = _takeViewModels.DestinationsOrNull();
            switch (destinations)
            {
                case null:
                    ViewData["Message"] = "There are no destinations.";
                    return View("Error");
                default:
                    return View(destinations);
            }
        }
    }
}
