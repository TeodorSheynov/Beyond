using System.Linq;
using Beyond.Data;
using Beyond.Services.Interfaces;

namespace Beyond.Services
{
    public class DeleteAndSaveEntity :IDeleteAndSaveEntity
    {
        private readonly ITakeEntityById _takeEntityById;
        private readonly ApplicationDbContext _context;
        public DeleteAndSaveEntity(ITakeEntityById takeEntityById, ApplicationDbContext context)
        {
            _takeEntityById = takeEntityById;
            _context = context;
        }
        public void Ticket(string id)
        {
            var ticketToDelete = _takeEntityById.Ticket(id);
            var vehicle = ticketToDelete.Vehicle;
            vehicle.Seats.First(s => s.SeatNumber == ticketToDelete.Seat).IsTaken = false;
            _context.Tickets.Remove(ticketToDelete);
            _context.Vehicles.Update(vehicle);
            _context.SaveChanges();
        }
    }
}