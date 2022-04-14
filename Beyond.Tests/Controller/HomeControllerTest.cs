using Beyond.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Beyond.Tests.Controller
{
    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnView()
        {
            //Arrange
            var homeController = new HomeController();
            //Act
            var result=homeController.Index();
            //Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
