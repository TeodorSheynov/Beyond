using System.Collections.Generic;
using Beyond.Data.Models;
using Beyond.Models;
using Beyond.Models.Control;
using Beyond.Models.Crew;
using Beyond.Models.Destination;

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

    }
}