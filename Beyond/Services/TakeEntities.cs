using System.Collections.Generic;
using System.Linq;
using Beyond.Data;
using Beyond.Models.Control;
using Beyond.Services.Interfaces;

namespace Beyond.Services
{
    public class TakeEntities : ITakeEntities
    {
        private readonly ApplicationDbContext _context;
        public TakeEntities(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<ControlPilotsViewModel> TakeControlPilotsOrNull()
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

        public List<ControlDestinationsViewModel> TakeControlDestinationsOrNull()
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
    }
}