using System;
using System.Collections.Generic;
using System.Linq;
using Beyond.Data.Models.Enums;
using Beyond.Models.Control;
using Beyond.Services;
using Xunit;

namespace Beyond.Tests.Services
{
    public class TakeRanksTests
    {
        [Fact]
        public void UiRankDecoratorShouldReturnDecoratedRank()
        {
            var service = new TakeRanks();
            var rank = Rank.SecondOfficer.ToString();
            const string expected = "Second Officer";
            var decoratedRank = service.UiRankDecorator(rank);
            Assert.Equal(expected,decoratedRank);
        }

        [Fact]
        public void PilotRankNamesShouldReturnListOfPilotRankViewModels()
        {
            var service = new TakeRanks();
            var rankNames = service.PilotRankNames(); 
            var expected = Enum.GetNames(typeof(Rank)).Count();
            Assert.NotNull(rankNames);
            Assert.IsType<List<PilotRanksViewModel>>(rankNames);
            Assert.Equal(expected,rankNames.Count);
        }
    }
}