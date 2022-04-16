using System;
using Beyond.Models.DTOs.Input;
using Beyond.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Beyond.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EditController : Controller
    {
        private readonly ITakeModels _takeViewModels;
        private readonly ITakeRanks _takeRanks;
        private readonly ICreateDto _createDto;
        private readonly IUpdateEntity _update;
        private readonly ITakeEntityById _entityById;
        public EditController(
            ITakeModels takeViewModels,
            ITakeRanks takeRanks,
            ICreateDto createDto,
            IUpdateEntity update,
            ITakeEntityById entityById)
        {
            _takeViewModels = takeViewModels;
            _takeRanks = takeRanks;
            _createDto = createDto;
            _update = update;
            _entityById = entityById;
        }
        // GET
        public IActionResult VehicleAll()
        {
            var vehicles = _takeViewModels.VehiclesForEditOrNull();
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
            try
            {
                _entityById.Vehicle(id);
            }
            catch (Exception)
            {
                ViewData["Message"] = "Not Found";
                return View("Error");
            }
            ViewBag.Destinations = _takeViewModels.ControlDestinationsOrNull();
            ViewBag.Pilots = _takeViewModels.AvailablePilotsOrNull(pilotId);
            ViewBag.Id = id;
            var dto = _createDto.Vehicle(id);

            return View(dto);
        }
        [HttpPost]
        public IActionResult Vehicle([FromForm] VehicleDto dto, string id)
        {
            try
            {
                _entityById.Vehicle(id);
            }
            catch (Exception)
            {
                ViewData["Message"] = "Something went wrong while updating the data.";
                return View("Error");
            }

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
            var destinations = _takeViewModels.DestinationsForEditOrNull();
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
            try
            {
                _entityById.Destination(id);
            }
            catch (Exception)
            {
                ViewData["Message"] = "Not Found";
                return View("Error");
            }
            ViewBag.Id=id;
            var dto = _createDto.Destination(id);
            return View(dto);
        }
        [HttpPost]
        public IActionResult Destination([FromForm] DestinationDto dto, string id)
        {
            try
            {
                _entityById.Destination(id);
            }
            catch (Exception)
            {
                ViewData["Message"] = "Something went wrong while updating the data.";
                return View("Error");
            }

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
            var pilots = _takeViewModels.PilotsForEditOrNull();
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
            try
            {
                _entityById.Pilot(id);
            }
            catch (Exception)
            {
                ViewData["Message"] = "Not Found";
                return View("Error");
            }

            ViewData["ranks"] = _takeRanks.PilotRankNames();
            ViewBag.Id = id;
            var pilot = _createDto.Pilot(id);
            return View(pilot);
        }
        [HttpPost]
        public IActionResult Pilot([FromForm] PilotDto dto, string id)
        {
            try
            {
                _entityById.Pilot(id);
            }
            catch (Exception)
            {
                ViewData["Message"] = "Something went wrong while updating the data.";
                return View("Error");
            }

            if (!ModelState.IsValid)
            {
                ViewData["ranks"] = _takeRanks.PilotRankNames();
                ViewBag.Id = id;
                return View(dto);
            }
            _update.Pilot(dto, id);
            return RedirectToAction("PilotsAll");
        }
    }
}