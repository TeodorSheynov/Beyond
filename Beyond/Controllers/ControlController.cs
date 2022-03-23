using System;
using System.Linq;
using Beyond.Data;
using Beyond.Data.Models;
using Beyond.Data.Models.Enums;
using Beyond.Models;
using Beyond.Models.Control;
using Beyond.Models.DTOs;
using Beyond.Services;
using Beyond.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Beyond.Controllers
{
    public class ControlController : Controller
    {
        private ApplicationDbContext _context;
        private readonly IEnumNames _enumValues;
        public ControlController(ApplicationDbContext context, IEnumNames enumValues)
        {
            _context = context;
            _enumValues = enumValues;
        }
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Vehicle()
        {
            var destinations = _context
                .Destinations
                .Select(x => new DestinationsViewModel()
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
            ViewData["destinations"] = destinations;
            ViewData["pilots"] = pilots;
            return PartialView("_VehiclePartial");
        }
        [HttpPost]
        public IActionResult Vehicle([FromForm] VehicleDto formData)
        {
            var model = formData;
            return Ok();
        }

        public IActionResult Pilot()
        {
            var ranks= _enumValues.EnumRankNames();
           
            ViewData["ranks"] = ranks;
            return PartialView("_PilotPartial");
        }
        [HttpPost]
        public IActionResult Pilot([FromForm] PilotDto formData)
        {
            var model = formData;
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
    }
}
