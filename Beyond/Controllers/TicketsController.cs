using System.Linq;
using Beyond.Data;
using Beyond.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Beyond.Controllers
{
    public class TicketsController : Controller
    {
        private static ApplicationDbContext _context;

        public TicketsController(ApplicationDbContext ctx)
        {
            _context = ctx;
        }
        [Authorize]
        public IActionResult All()
        {
            var tikets = _context
                .Tickets
                .Select(x => new TicketViewModel()
                {
                    Id = x.Id,
                    Description = x.Description,
                    Name = x.Destination.Name,
                    Path = x.ImgPath,
                    Price = $"{x.Destination.Price}$"
                }).ToList();
            return View(tikets);
        }
        [Authorize]
        public IActionResult More(string id)
        {
            ViewData["id"] = id;
            return View();
        }
    }
}
