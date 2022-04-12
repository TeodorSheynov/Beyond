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
        private readonly ApplicationDbContext _context;
        private readonly ITakeEntityById _takeEntityById;
        private readonly ITakeViewModels _takeViewModels;
        private readonly IMapper _mapper;
        public EditController(ApplicationDbContext context, ITakeEntityById takeEntityById, ITakeViewModels takeViewModels, IMapper mapper)
        {
            _context = context;
            _takeEntityById = takeEntityById;
            _takeViewModels = takeViewModels;
            _mapper = mapper;
        }
        // GET
        public IActionResult VehicleAll()
        {
            var vehicles = _context
                .Vehicles
                .Select(v => new EditVehicleViewModel()
                {
                    Id = v.Id,
                    Name = v.Name,
                    Speed = v.Speed,
                    Pilot = new ControlPilotsViewModel()
                    {
                        Id = v.PilotId,
                        Name = v.Pilot.Name
                    },
                    SerialNumber = v.SerialNumber,
                    Seats = v.Seats.Count,
                    Departure = v.Departure,
                    Arrival = v.Arrival,
                    Destination = new ControlDestinationsViewModel()
                    {
                        Id = v.DestinationId,
                        Name = v.Destination.Name
                    },
                    LaunchSite = v.LaunchSite,
                }).ToList();
            return View(vehicles);
        }

        public IActionResult Vehicle(string id, string pilotId)
        {
            ViewBag.Destinations = _takeViewModels.ControlDestinationsOrNull();
            ViewBag.Pilots = _takeViewModels.EditPilotsOrNull(pilotId);

            ViewBag.Id = id;
            var dto = _context
                .Vehicles
                .Where(x => x.Id == id)
                .ToArray()
                .Select(x => _mapper.Map<VehicleDto>(x))
                .Single();

            return View(dto);
        }
        [HttpPost]
        public IActionResult Vehicle([FromForm] VehicleDto dto, string id)
        {
            var vehicle = _takeEntityById.Vehicle(id);
            if (!ModelState.IsValid)
            {
                ViewBag.Destinations = _takeViewModels.ControlDestinationsOrNull();
                ViewBag.Pilots = _takeViewModels.EditPilotsOrNull(dto.PilotId);
                ViewBag.Id = id;
                return View(dto);
            }
            _mapper.Map(dto, vehicle);
            _context.Vehicles.Update(vehicle);
            _context.SaveChanges();
            return RedirectToAction("VehicleAll");
        }

        public IActionResult DestinationsAll()
        {
            var destinations = _context
                .Destinations
                .Select(x => _mapper.Map<EditDestinationViewModel>(x))
                .ToList();
            return View(destinations);
        }

        public IActionResult Destination(string id)
        {

            ViewBag.Id = id;
            var dto = _context
                .Destinations
                .Where(x => x.Id == id)
                .ToArray()
                .Select(d => _mapper.Map<DestinationDto>(d))
                .Single();
            return View(dto);
        }
        [HttpPost]
        public IActionResult Destination([FromForm] DestinationDto dto, string id)
        {
            var destination = _takeEntityById.Destination(id);
            if (!ModelState.IsValid)
            {
                ViewBag.Id = id;
                return View(dto);
            }

            _mapper.Map(dto, destination);
            _context.Destinations.Update(destination);
            _context.SaveChanges();
            return RedirectToAction("DestinationsAll");
        }
    }
}