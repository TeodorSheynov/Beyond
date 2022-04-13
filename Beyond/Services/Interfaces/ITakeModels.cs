using System.Collections.Generic;
using Beyond.Models.Control;
using Beyond.Models.Crew;
using Beyond.Models.Destination;
using Beyond.Models.DTOs.Output;
using Beyond.Models.Ticket;

namespace Beyond.Services.Interfaces
{
    public interface ITakeModels
    {
        public List<ControlPilotsViewModel> ControlPilotsOrNull();
        public List<ControlDestinationsViewModel> ControlDestinationsOrNull();
        public List<ControlPilotsViewModel> AvailablePilotsOrNull(string id);

        public List<CrewViewModel> CrewOrNull();
        public List<DestinationViewModel> DestinationsOrNull();
        public List<TicketViewModel> TicketsOrNull();
        public List<MyTicketViewModel> MyTicketsOrNull();

        public List<EditVehicleViewModel> VehiclesForEditOrNull();
        public List<EditDestinationViewModel> DestinationsForEditOrNull();
        public List<EditPilotViewModel> PilotsForEditOrNull();

    }
}