using System.Linq;
using Beyond.Data;
using Beyond.Models.Destination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Beyond.Controllers
{
    public class DestinationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DestinationsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize]
        [Route("/Destinations")]
        public IActionResult Destinations()
        {
            var destinations = _context
                .Destinations
                .Select(x => new DestinationViewModel()
                {
                    Description = x.Description,
                    Id = x.Id,
                    Name = x.Name,
                    Url = x.Url
                }).ToList();
            return View(destinations);
        }
    }
}
