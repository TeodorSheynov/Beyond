using System.Collections.Generic;
using Beyond.Models.Control;

namespace Beyond.Services.Interfaces
{
    public interface ITakeRanks
    {
        public List<PilotRanksViewModel> PilotRankNames();
        public string UiRankDecorator(string rank);
    }
}