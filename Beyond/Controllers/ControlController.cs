using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Beyond.Data;
using Beyond.Data.Models;
using Beyond.Data.Models.Enums;
using Beyond.Models;
using Beyond.Models.Control;
using Beyond.Models.DTOs;
using Beyond.Services;
using Beyond.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Beyond.Controllers
{
    public class ControlController : Controller
    {
        private ApplicationDbContext _context;
        private readonly IEnumNames _enumValues;
        private readonly ITakeEntityById _entityById;
        public ControlController(
            ApplicationDbContext context,
            IEnumNames enumValues,
            ITakeEntityById entityById)
        {
            _context = context;
            _enumValues = enumValues;
            _entityById = entityById;
        }
        public IActionResult Index()
        {

            return View();
        }
        
        public IActionResult Vehicle()
        {
            var destinations = _context
                .Destinations
                .Select(x => new ControlDestinationsViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();
            var pilots = _context
                .Pilots
                .Select(x => new ControlPilotsViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();
            if (pilots.Count==0 )
            {
                ViewBag.PilotIsNull=true;
            }
            else
            {
                ViewBag.PilotIsNull = false;
                ViewData["pilots"] = pilots;
            }

            if (destinations.Count==0)
            {
                ViewBag.DestinationIsNull = true;
            }
            else
            {
                ViewBag.DestinationIsNull = false;
                ViewData["destinations"] = destinations;
            }
            return View();
        }
        [HttpPost]
        public IActionResult Vehicle([FromForm] VehicleDto formData)
        {
            var model = formData;
            if (!ModelState.IsValid)
            {
             
                return View(formData);
            }

            var vehicle = new Vehicle
            {
                Arrival = DateTime.Parse(model.Arrival),
                Departure = DateTime.Parse(model.Departure),
                Destination = _entityById.Destination(model.DestinationId),
                DestinationId = model.DestinationId,
                LaunchSite = model.LaunchSite,
                Name = model.Model,
                OnFLight = false,
                Pilot = _entityById.Pilot(model.PilotId),
                PilotId = model.PilotId,
                Seats = model.Seats,
                SerialNumber = model.SerialNumber,
                Speed = model.Speed,
            };
            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();
            return View("Index");
        }

        public IActionResult Pilot()
        {
            var ranks= _enumValues.EnumRankNames();
           
            ViewData["ranks"] = ranks;
            return View();
        }
        [HttpPost]
        public IActionResult Pilot([FromForm] PilotDto formData)
        {
            var model = formData;
            if (ModelState.IsValid)
            {
                var pilot = new Pilot()
                {
                    Age = model.Age,
                    Description = model.Description,
                    ImgPath = model.Url,
                    Rank = model.Rank,
                    Name = model.Name
                };
                _context.Pilots.Add(pilot);
                _context.SaveChanges();
                return View("Index");
            }
            var ranks = _enumValues.EnumRankNames();

            ViewData["ranks"] = ranks;
            return View(formData);

        }

        public IActionResult Destination()
        {
            return PartialView("_DestinationPartial");
        }
        [HttpPost]
        public IActionResult Destination([FromForm] DestinationDto formData)
        {
            var model = formData;
            var destination = new Destination
            {
                Name = model.Name,
                Description = model.Description,
                Distance = model.Distance,
                Price = model.Price,
                Url = model.Url
            };
            _context.Destinations.Add(destination);
            _context.SaveChanges();
            return Redirect("/Control");
        }
    }
}
