using Beyond.Models.DTOs;
using Beyond.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Beyond.Controllers
{
    public class ControlController : Controller
    {
        private readonly IEnumNames _enumValues;
        private readonly ITakeEntities _takeEntities;
        private readonly ICreateAndSaveEntity _createAndSaveEntity;
        public ControlController(IEnumNames enumValues, ITakeEntities takeEntities, ICreateAndSaveEntity createAndSaveEntity)
        {
            _enumValues = enumValues;
            _takeEntities = takeEntities;
            _createAndSaveEntity = createAndSaveEntity;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Vehicle()
        {
            ViewBag.Destinations = _takeEntities.TakeControlDestinationsOrNull();
            ViewBag.Pilots = _takeEntities.TakeControlPilotsOrNull();
            return View();
        }
        [HttpPost]
        public IActionResult Vehicle([FromForm] VehicleDto formData)
        {
            switch (ModelState.IsValid)
            {
                case false:
                    ViewBag.Destinations = _takeEntities.TakeControlDestinationsOrNull();
                    ViewBag.Pilots = _takeEntities.TakeControlPilotsOrNull();
                    return View(formData);
                default:
                    _createAndSaveEntity.Vehicle(formData);

                    return View("Index");
            }
        }

        public IActionResult Pilot()
        {
            var ranks = _enumValues.EnumRankNames();

            ViewData["ranks"] = ranks;
            return View();
        }
        [HttpPost]
        public IActionResult Pilot([FromForm] PilotDto formData)
        {
            switch (ModelState.IsValid)
            {
                case false:
                    {
                        var ranks = _enumValues.EnumRankNames();

                        ViewData["ranks"] = ranks;
                        return View(formData);
                    }
                default:
                    _createAndSaveEntity.Pilot(formData);
                    return View("Index");
            }
        }

        public IActionResult Destination()
        {
            return View();
        }
        [HttpPost]
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
