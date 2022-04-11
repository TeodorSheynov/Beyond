using System.Collections.Generic;
using Beyond.Models.Control;
using Beyond.Models.Crew;
using Beyond.Models.Destination;
using Beyond.Models.Ticket;

namespace Beyond.Services.Interfaces
{
    public interface ITakeViewModels
    {
        public List<ControlPilotsViewModel> ControlPilotsOrNull();
        public List<ControlDestinationsViewModel> ControlDestinationsOrNull();
        public List<CrewViewModel> CrewMembersOrNull();
        public List<DestinationViewModel> DestinationsOrNull();
        public List<TicketViewModel> TicketsOrNull();
        public List<MyTicketViewModel> MyTicketOrNull();
        public List<ControlPilotsViewModel> EditPilotsOrNull(string id);

    }
}