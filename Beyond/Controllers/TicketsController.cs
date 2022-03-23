using System.Linq;
using System.Security.Claims;

using Beyond.Data;
using Beyond.Data.Models;
using Beyond.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Beyond.Controllers
{
    public class TicketsController : Controller
    {
        private  ApplicationDbContext _context;
        private  UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public TicketsController(
            ApplicationDbContext ctx,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _context = ctx;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Authorize]
        public IActionResult All()
        {
            var destinations = _context
                .Destinations
                .Select(x => new TicketViewModel()
                {
                    Description = x.Description,
                    Id = x.Id,
                    Name = x.Name,
                    Path = x.Url,
                    Price = $"{x.Price}$"
                }).ToList();
            return View(destinations);
        }

        [Authorize]
        public IActionResult MyTickets()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUser = _context.Users.Include(host => host.Tickets).ToList();
            var user = currentUser.FirstOrDefault(u=>u.Id==currentUserId);
            if (user == null) return View("Error");
            {
                var tickets = _context.Tickets
                    .Where(x => x.UserId == currentUserId)
                    .Select(x => new MyTicketViewModel()
                    {
                        Departure = x.Vehicle.Departure.ToLongDateString(),
                        Name = x.Vehicle.Destination.Name,
                        Price = x.Vehicle.Destination.Price,
                        SeatNumber = x.Vehicle.Seats.ToString(),
                        SerialNumber = x.Vehicle.SerialNumber
                    });
                return View(tickets);
            }

        }

        [Authorize]
        public IActionResult Buy(string id)
        {
            var user1 = _context.UserClaims.Select(x => x.UserId);
            var user = _context
                .Users
                .FirstOrDefault(x => x.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

            var destination = _context
                .Destinations
                .FirstOrDefault(x => x.Id == id);
            if (destination==null || user ==null)
            {
                return View("Error");
            }

            var vehicle = _context
                .Vehicles
                .FirstOrDefault(x => x.Destination.Id==id);

            if (vehicle != null)
            {
                var ticket = new Ticket
                {
                    Description = destination.Description,
                    ImgPath = destination.Url,
                    User = user,
                    UserId = user.Id,
                    Vehicle = vehicle,
                    VehicleId = vehicle.Id,

                };
                _context.Tickets.Add(ticket);
            }

            _context.SaveChangesAsync();

            var userTIckets=user.Tickets;

            ViewData["id"] = id;
            return RedirectToAction("MyTickets");
        }
    }
}
