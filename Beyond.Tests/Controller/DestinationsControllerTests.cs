using System.Collections.Generic;
using System.Linq;
using Beyond.Controllers;
using Beyond.Models.Crew;
using Beyond.Models.Destination;
using Beyond.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Beyond.Tests.Controller
{
    public class DestinationsControllerTests
    {
        [Fact]
        public void DestinationsControllerShouldReturnDefaultViewWhenThereAreDestinations()
        {
            var takeModelInstance = TakeModelsMock.DestinationsOrNullPopulatedInstance;
            var controller = new DestinationsController(takeModelInstance);
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
            var controller = new DestinationsController(TakeModelsMock.DestinationsOrNullEmptyInstance);
            var result = controller.Destinations();

            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Error", viewResult.ViewName);
        }
    }
}