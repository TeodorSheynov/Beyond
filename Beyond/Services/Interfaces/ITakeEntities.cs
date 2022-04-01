using System.Collections.Generic;
using Beyond.Models.Control;

namespace Beyond.Services.Interfaces
{
    public interface ITakeEntities
    {
        public List<ControlPilotsViewModel> TakeControlPilotsOrNull();
        public List<ControlDestinationsViewModel> TakeControlDestinationsOrNull();
    }
}