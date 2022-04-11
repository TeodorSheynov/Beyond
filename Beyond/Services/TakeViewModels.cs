using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

using Beyond.Data;
using Beyond.Models;
using Beyond.Models.Control;
using Beyond.Models.Crew;
using Beyond.Models.Destination;
using Beyond.Models.Ticket;
using Beyond.Services.Interfaces;

using Microsoft.AspNetCore.Http;

namespace Beyond.Services
{
    public class TakeViewModels : ITakeViewModels
    {
        private readonly ApplicationDbContext _context;
        private readonly IEnumNames _enumNames;
        private readonly IHttpContextAccessor _accessor;
        private readonly ITakeEntityById _takeEntityById;
        public TakeViewModels(ApplicationDbContext context,
            IEnumNames enumNames,
            IHttpContextAccessor accessor, 
            ITakeEntityById takeEntityById)
        {
            _context = context;
            _enumNames = enumNames;
            _accessor = accessor;
            _takeEntityById = takeEntityById;
        }
        public List<ControlPilotsViewModel> ControlPilotsOrNull()
        {
            var pilots = _context
                .Pilots
                .Where(p=>p.Vehicle==null)
                .Select(x => new ControlPilotsViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();

            return !pilots.Any() ? null : pilots;

        }
        public List<ControlPilotsViewModel> EditPilotsOrNull(string id)
        {
            var pilots = _context
                .Pilots
                .Where(p => p.Vehicle == null || p.Id==id)
                .Select(x => new ControlPilotsViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();

            return !pilots.Any() ? null : pilots;

        }

        public List<ControlDestinationsViewModel> ControlDestinationsOrNull()
        {
            var destinations = _context
                .Destinations
                .Select(x => new ControlDestinationsViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();

            return !destinations.Any() ? null : destinations;
        }

        public List<CrewViewModel> CrewMembersOrNull()
        {
            var crew = _context
                .Pilots
                .Select(x => new CrewViewModel()
                {
                    Description = x.Description,
                    Name = x.Name,
                    Rank = _enumNames.UiRankDecorator(x.Rank.ToString()),
                    Url = x.ImgPath
                }).ToList();

            return !crew.Any() ? null : crew;
        }

        public List<DestinationViewModel> DestinationsOrNull()
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

            return !destinations.Any() ? null : destinations;
        }

        public List<TicketViewModel> TicketsOrNull()
        {
            var ticketViewModel = _context
                .Vehicles
                .Select(x => new TicketViewModel()
                {
                    Id = x.Id,
                    Name = x.Destination.Name,
                    Path = x.Destination.Url,
                    Price = $"{x.Destination.Price}$",
                    Date = x.Departure,
                    TicketsLeft = x.Seats.Count(s => s.IsTaken == false),
                    LaunchSite = x.LaunchSite

                })
                .ToList();

            return !ticketViewModel.Any() ? null : ticketViewModel;
        }

        public List<MyTicketViewModel> MyTicketOrNull()
        {
            if (_accessor.HttpContext == null)
                throw new ArgumentNullException($"HttpContext is null.");

            var userId = _accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _takeEntityById.User(userId);
            var tickets = user
                .Tickets
                .Select(x => new MyTicketViewModel()
                {
                    Id = x.Id,
                    Departure = x.Vehicle.Departure,
                    Name = x.Vehicle.Destination.Name,
                    Price = x.Vehicle.Destination.Price,
                    SeatNumber = x.Seat.ToString(),
                    SerialNumber = x.Vehicle.SerialNumber
                }).ToList();
            return !tickets.Any() ? null : tickets;

        }
    }
}