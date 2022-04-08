using Beyond.Data.DTOs;
using Beyond.Data.Models;

namespace Beyond.Services.Interfaces
{
    public interface ICreateAndSaveEntity
    {
        public void Vehicle(VehicleDto dto);
        public void Destination(DestinationDto dto);
        public void Pilot(PilotDto dto);
        public void Ticket(User user, Vehicle vehicle);
    }
}