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
            var vehicleFlights = _context
                .Vehicles
                .Select(x => new TicketViewModel()
                {
                    Id = x.Id,
                    Name = x.Destination.Name,
                    Path = x.Destination.Url,
                    Price = $"{x.Destination.Price}$",
                    Date = x.Departure,
                    TicketsLeft = x.Seats,
                    LaunchSite = x.LaunchSite
                    
                })
                .ToList();
            return View(vehicleFlights);
        }
        [Authorize]
        public IActionResult Search(string id)
        {
            var vehicleFlights = _context
                .Vehicles
                .Where(v=>v.Destination.Id==id)
                .Select(x => new TicketViewModel()
                {
                    Id = x.Id,
                    Name = x.Destination.Name,
                    Path = x.Destination.Url,
                    Price = $"{x.Destination.Price}$",
                    Date = x.Departure,
                    TicketsLeft = x.Seats,
                    LaunchSite = x.LaunchSite

                })
                .ToList();
            return View("All",vehicleFlights);
        }

        [Authorize]
        public IActionResult MyTickets()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) return View("Error");
            {
                var tickets = user
                    .Tickets
                    .Select(x => new MyTicketViewModel()
                    {
                        Id = x.Id,
                        Departure = x.Vehicle.Departure,
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
            var user = _context
                .Users
                .FirstOrDefault(x => x.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

            var vehicle = _context
                .Vehicles
                .FirstOrDefault(x => x.Id == id);

            if (vehicle != null)
            {
                var ticket = new Ticket
                {
                    Description = vehicle.Destination.Description,
                    ImgPath = vehicle.Destination.Url,
                    User = user,
                    Vehicle = vehicle,

                };
                _context.Tickets.Add(ticket);
                vehicle.Seats--;
            }
            _context.SaveChangesAsync();

            

            ViewData["id"] = id;
            return RedirectToAction("MyTickets");
        }
        public IActionResult Delete(string id)
        {
            var ticketToDelete = _context
                .Tickets
                .FirstOrDefault(t => t.Id == id);
            if (ticketToDelete == null) return View("Error");  
            _context.Tickets.Remove(ticketToDelete);
            _context.SaveChanges();

            return RedirectToAction("MyTickets");
        }
    }
}
