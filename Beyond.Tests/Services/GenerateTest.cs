using System.Collections.Generic;
using Beyond.Data.Models;
using Beyond.Services;
using Beyond.Services.Interfaces;
using Xunit;

namespace Beyond.Tests.Services
{
    public class GenerateTest
    {
        [Fact]
        public void GenerateShouldGenerateTheGivenCountSeats()
        {
            var generator = new Generate();
            const int count = 10;
            var seats= generator.Seats(count);
            Assert.NotNull(seats);
            Assert.IsType<List<Seat>>(seats);
            Assert.Equal(10,seats.Count);
        }
    }
}
