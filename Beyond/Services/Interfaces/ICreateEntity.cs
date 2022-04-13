using System.Threading.Tasks;
using Beyond.Data.Models;
using Beyond.Models.DTOs;
using Beyond.Models.DTOs.Input;

namespace Beyond.Services.Interfaces
{
    public interface ICreateEntity
    {
        public Task<int> Vehicle(VehicleDto dto);
        public void Destination(DestinationDto dto);
        public void Pilot(PilotDto dto);
        public void Ticket(User user, Vehicle vehicle);
    }
}