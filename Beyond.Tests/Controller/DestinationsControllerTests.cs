using System.Collections.Generic;
using System.Linq;
using Beyond.Controllers;
using Beyond.Models.Crew;
using Beyond.Models.Destination;
using Beyond.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Xunit;

namespace Beyond.Tests.Controller
{
    public class DestinationsControllerTests
    {
        [Fact]
        public void DestinationsControllerShouldReturnDefaultViewWhenThereAreDestinations()
        {
            var takeModelInstance = TakeModelsMock.DestinationsOrNullPopulatedInstance;
            var cache = new MemoryCache(new MemoryCacheOptions());
            var controller = new DestinationsController(takeModelInstance,cache);
            //Act
            var result = controller.Destinations();

            //Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<DestinationViewModel>>(viewResult.ViewData.Model);
            Assert.Equal(5, model.Count());
        }

        [Fact]
        public void DestinationsControllerShouldReturnErrorViewWhenThereAreNoDestinations()
        {
            var cache = new MemoryCache(new MemoryCacheOptions());
            var controller = new DestinationsController(TakeModelsMock.DestinationsOrNullEmptyInstance, cache);
            var result = controller.Destinations();

            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Error", viewResult.ViewName);
        }
    }
}