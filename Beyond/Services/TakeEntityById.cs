using System;
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
                .FirstOrDefault(x => x.Id == id);
            return destination ?? throw new ArgumentNullException($"Unknown destination. Destination id: {id}");
        }

        public Pilot Pilot(string id)
        {
            var pilot = _context
                .Pilots
                .FirstOrDefault(p => p.Id == id);
            return pilot ?? throw new ArgumentNullException($"Unknown pilot. Pilot id: {id}");
        }
    }
}