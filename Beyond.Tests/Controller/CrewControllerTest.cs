using System.Collections.Generic;
using System.Linq;
using Beyond.Controllers;
using Beyond.Models.Crew;
using Beyond.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Beyond.Tests.Controller
{
    public class CrewControllerTest
    {
        [Fact]
        public void CrewShouldReturnView()
        {
            //Arrange
            var takeModelInstance = TakeModelsMock.CrewOrNullPopulatedInstance;
            var controller = new CrewController(takeModelInstance);
            //Act
            var result = controller.Crew();

            //Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<CrewViewModel>>(viewResult.ViewData.Model);
            Assert.Equal(5,model.Count());
        }

        [Fact]
        public void CrewShouldReturnViewErrorWhenCollectionIsNull()
        {
            var controller = new CrewController(TakeModelsMock.CrewOrNullEmptyInstance);
            var result= controller.Crew();

            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Error",viewResult.ViewName);


        }
    }
}