using Beyond.Data.Models;

namespace Beyond.Services.Interfaces
{
    public interface ITakeEntityById
    {
        public Destination Destination(string id);
        public Pilot Pilot(string id);
        public User User(string id);
        public Vehicle Vehicle(string id);
        public Ticket Ticket(string id);
    }
}