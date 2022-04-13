using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Beyond.Data;
using Beyond.Data.Models;
using Beyond.Models.Control;
using Beyond.Models.DTOs.Input;
using Beyond.Models.DTOs.Output;
using Beyond.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Beyond.Controllers
{
    public class EditController : Controller
    {
        private readonly ITakeViewModels _takeViewModels;
        private readonly ITakeRanks _takeRanks;
        private readonly ICreateDto _createDto;
        private readonly IUpdateEntity _update;
        public EditController(
            ITakeViewModels takeViewModels,
            ITakeRanks takeRanks, 
            ICreateDto createDto, 
            IUpdateEntity update)
        {
            _takeViewModels = takeViewModels;
            _takeRanks = takeRanks;
            _createDto = createDto;
            _update = update;
        }
        // GET
        public IActionResult VehicleAll()
        {
            var vehicles = _takeViewModels.EditVehicleOrNull();
            switch (vehicles)
            {
                case null:
                    ViewData["Message"] = "There are no vehicles to edit.";
                    return View("Error");
                default:
                    return View(vehicles);
            }
        }

        public IActionResult Vehicle(string id, string pilotId)
        {
            ViewBag.Destinations = _takeViewModels.ControlDestinationsOrNull();
            ViewBag.Pilots = _takeViewModels.AvailablePilotsOrNull(pilotId);

            ViewBag.Id = id;
            var dto = _createDto.Vehicle(id);

            return View(dto);
        }
        [HttpPost]
        public IActionResult Vehicle([FromForm] VehicleDto dto, string id)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Destinations = _takeViewModels.ControlDestinationsOrNull();
                ViewBag.Pilots = _takeViewModels.AvailablePilotsOrNull(dto.PilotId);
                ViewBag.Id = id;
                return View(dto);
            }
            _update.Vehicle(dto, id);
            return RedirectToAction("VehicleAll");
        }

        public IActionResult DestinationsAll()
        {
            var destinations = _takeViewModels.EditDestinationOrNull();
            switch (destinations)
            {
                case null:
                    ViewData["Message"] = "There are no destinations to edit";
                    return View("Error");
                default:
                    return View(destinations);
            }
        }

        public IActionResult Destination(string id)
        {

            ViewBag.Id = id;
            var dto = _createDto.Destination(id);
            return View(dto);
        }
        [HttpPost]
        public IActionResult Destination([FromForm] DestinationDto dto, string id)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Id = id;
                return View(dto);
            }

            _update.Destination(dto, id);
            return RedirectToAction("DestinationsAll");
        }

        public IActionResult PilotsAll()
        {
            var pilots = _takeViewModels.EditPilotsOrNull();
            switch (pilots)
            {
                case null:
                    ViewData["Message"] = "There are no pilots to edit";
                    return View("Error");
                default:
                    return View(pilots);
            }
        }

        public IActionResult Pilot(string id)
        {
            ViewData["ranks"] = _takeRanks.PilotRankNames();
            ViewBag.Id = id;
            var pilot= _createDto.Pilot(id);
            return View(pilot);
        }
        [HttpPost]
        public IActionResult Pilot([FromForm] PilotDto dto, string id)
        {
            
            if (!ModelState.IsValid)
            {
                ViewData["ranks"] = _takeRanks.PilotRankNames();
                ViewBag.Id = id;
                return View(dto);
            }
            _update.Pilot(dto,id);
            return RedirectToAction("PilotsAll");
        }
    }
}