using System.Threading.Tasks;
using Beyond.Models.DTOs;
using Beyond.Models.DTOs.Input;
using Beyond.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace Beyond.Controllers
{
    public class ControlController : Controller
    {
        private readonly ITakeRanks _enumValues;
        private readonly ITakeModels _takeViewModels;
        private readonly ICreateEntity _createAndSaveEntity;
        public ControlController(ITakeRanks enumValues, ITakeModels takeViewModels, ICreateEntity createAndSaveEntity)
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
        public async Task<IActionResult> Vehicle([FromForm] VehicleDto formData)
        {
            switch (ModelState.IsValid)
            {
                case false:
                    ViewBag.Destinations = _takeViewModels.ControlDestinationsOrNull();
                    ViewBag.Pilots = _takeViewModels.ControlPilotsOrNull();
                    return View(formData);
                default:
                    await _createAndSaveEntity.Vehicle(formData);

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
