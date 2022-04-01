using Beyond.Models.DTOs;

namespace Beyond.Services.Interfaces
{
    public interface ICreateAndSaveEntity
    {
        public void Vehicle(VehicleDto dto);
        public void Destination(DestinationDto dto);
        public void Pilot(PilotDto dto);

    }
}