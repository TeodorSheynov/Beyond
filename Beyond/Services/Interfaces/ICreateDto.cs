using Beyond.Models.DTOs.Input;

namespace Beyond.Services.Interfaces
{
    public interface ICreateDto
    {
        public VehicleDto Vehicle(string id);
        public PilotDto Pilot(string id);
        public DestinationDto Destination(string id);

    }
}