using System.Linq;
using Beyond.Data;
using Beyond.Models;
using Beyond.Models.Control;
using Microsoft.AspNetCore.Mvc;

namespace Beyond.Controllers
{
    public class ControlController : Controller
    {
        private ApplicationDbContext _context;
        public ControlController(ApplicationDbContext context)
        {
            _context = context;
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
        public IActionResult Vehicle([FromForm] CreateVehicleViewModel createVehicleViewModel)
        {
            var model = createVehicleViewModel;
            return Ok();
        }

        public IActionResult Pilot()
        {
            return PartialView("_PilotPartial");
        }
     
    }
}
