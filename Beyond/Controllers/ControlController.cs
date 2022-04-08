using Beyond.Data.DTOs;
using Beyond.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace Beyond.Controllers
{
    public class ControlController : Controller
    {
        private readonly IEnumNames _enumValues;
        private readonly ITakeViewModels _takeViewModels;
        private readonly ICreateAndSaveEntity _createAndSaveEntity;
        public ControlController(IEnumNames enumValues, ITakeViewModels takeViewModels, ICreateAndSaveEntity createAndSaveEntity)
        {
            _enumValues = enumValues;
            _takeViewModels = takeViewModels;
            _createAndSaveEntity = createAndSaveEntity;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Vehicle()
        {
            ViewBag.Destinations = _takeViewModels.ControlDestinationsOrNull();
            ViewBag.Pilots = _takeViewModels.ControlPilotsOrNull();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Vehicle([FromForm] VehicleDto formData)
        {
            switch (ModelState.IsValid)
            {
                case false:
                    ViewBag.Destinations = _takeViewModels.ControlDestinationsOrNull();
                    ViewBag.Pilots = _takeViewModels.ControlPilotsOrNull();
                    return View(formData);
                default:
                    _createAndSaveEntity.Vehicle(formData);

                    return View("Index");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Pilot()
        {
            var ranks = _enumValues.PilotRankNames();

            ViewData["ranks"] = ranks;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Pilot([FromForm] PilotDto formData)
        {
            switch (ModelState.IsValid)
            {
                case false:
                    {
                        var ranks = _enumValues.PilotRankNames();

                        ViewData["ranks"] = ranks;
                        return View(formData);
                    }
                default:
                    _createAndSaveEntity.Pilot(formData);
                    return View("Index");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Destination()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Destination([FromForm] DestinationDto formData)
        {
            switch (ModelState.IsValid)
            {
                case false:
                    return View(formData);
                default:
                    _createAndSaveEntity.Destination(formData);
                    return View("Index");
            }
        }
    }
}
