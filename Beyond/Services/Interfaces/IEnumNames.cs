using System.Collections.Generic;
using Beyond.Models.Control;

namespace Beyond.Services.Interfaces
{
    public interface IEnumNames
    {
        public List<PilotRanksViewModel> EnumRankNames();
        public string UiRankDecorator(string rank);
    }
}