using Beyond.Models.DTOs.Input;

namespace Beyond.Services.Interfaces
{
    public interface IUpdateEntity
    {
        public void Vehicle(VehicleDto dto,string vehicleId);
        public void Pilot(PilotDto dto,string pilotId);
        public void Destination(DestinationDto dto,string destinationId);
    }
}