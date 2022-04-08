using System;
using System.Diagnostics;
using System.Linq;
using Beyond.Data;
using Beyond.Data.Models;
using Beyond.Services.Interfaces;

namespace Beyond.Services
{
    public class TakeEntityById:ITakeEntityById
    {
        private readonly ApplicationDbContext _context;
        public TakeEntityById(ApplicationDbContext context)
        {
            _context = context;
        }
        public Destination Destination(string id)
        {
            var destination = _context
                .Destinations
                .FirstOrDefault(d => d.Id == id);
            return destination ?? throw new ArgumentNullException($"Unknown destination. Destination id: {id}");
        }

        public Pilot Pilot(string id)
        {
            var pilot = _context
                .Pilots
                .FirstOrDefault(p => p.Id == id);
            return pilot ?? throw new ArgumentNullException($"Unknown pilot. Pilot id: {id}");
        }

        public User User(string id)
        {
           var user= _context
               .Users
               .FirstOrDefault(u => u.Id == id);
           return user ?? throw new ArgumentNullException($"Unknown user. User id: {id}");
        }

        public Vehicle Vehicle(string id)
        {
            var vehicle= _context
                .Vehicles
                .FirstOrDefault(v => v.Id == id);
            return vehicle ?? throw new ArgumentException($"Unknown vehicle. Vehicle id: {id}");
        }

        public Ticket Ticket(string id)
        {
            var ticket= _context
                .Tickets
                .FirstOrDefault(t => t.Id == id);
            return ticket ?? throw new ArgumentNullException($"Unknown ticket. Ticket id {id}");
        }
    }
}