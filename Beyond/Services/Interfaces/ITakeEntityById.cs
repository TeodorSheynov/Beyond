using Beyond.Data.Models;

namespace Beyond.Services.Interfaces
{
    public interface ITakeEntityById
    {
        public Destination Destination(string id);
        public Pilot Pilot(string id);

    }
}