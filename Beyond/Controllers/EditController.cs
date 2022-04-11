using System.Linq;
using AutoMapper;
using Beyond.Data;
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
                .Select(v => new VehicleViewModel()
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
            var vehicle = _takeEntityById.Vehicle(id);
           
            if (vehicle!=null)
            {
                ViewBag.Id = id;
                VehicleDto dto = new VehicleDto
                {
                    Name = vehicle.Name,
                    Speed = vehicle.Speed,
                    PilotId = vehicle.PilotId,
                    SerialNumber = vehicle.SerialNumber,
                    Seats = vehicle.Speed,
                    Departure = vehicle.Departure,
                    Arrival = vehicle.Arrival,
                    DestinationId = vehicle.DestinationId,
                    LaunchSite = vehicle.LaunchSite
                };
                return View(dto);
            }
            
            return View("Error");
        }
        [HttpPost]
        public IActionResult Vehicle([FromForm] VehicleDto dto,string id)
        {
            var vehicle = _takeEntityById.Vehicle(id);
            _mapper.Map(dto, vehicle);
            return View();
        }

    }
}