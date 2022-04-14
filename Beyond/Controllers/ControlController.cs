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
        private readonly ITakeRanks _ranks;
        private readonly ITakeModels _takeModels;
        private readonly ICreateEntity _creteEntity;
        public ControlController(
            ITakeRanks ranks, 
            ITakeModels takeModels, 
            ICreateEntity creteEntity)
        {
            _ranks = ranks;
            _takeModels = takeModels;
            _creteEntity = creteEntity;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Vehicle()
        {
            ViewBag.Destinations = _takeModels.ControlDestinationsOrNull();
            ViewBag.Pilots = _takeModels.ControlPilotsOrNull();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Vehicle([FromForm] VehicleDto formData)
        {
            switch (ModelState.IsValid)
            {
                case false:
                    ViewBag.Destinations = _takeModels.ControlDestinationsOrNull();
                    ViewBag.Pilots = _takeModels.ControlPilotsOrNull();
                    return View(formData);
                default:
                    await _creteEntity.Vehicle(formData);

                    return View("Index");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Pilot()
        {
            var ranks = _ranks.PilotRankNames();

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
                        var ranks = _ranks.PilotRankNames();

                        ViewData["ranks"] = ranks;
                        return View(formData);
                    }
                default:
                    _creteEntity.Pilot(formData);
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
                    _creteEntity.Destination(formData);
                    return View("Index");
            }
        }
    }
}
