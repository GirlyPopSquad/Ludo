using FluentAssertions;
using LudoAPI.Controllers;
using LudoAPI.Models;
using LudoAPI.Services;
using Moq;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
            // Act
            var result = _controller.GetStartingRoll(lobby);

            // Assert
            result.Should().BeOfType<ActionResult<Lobby>>().Which.Value.Should().BeEquivalentTo(lobby);
        }

        [Fact]
        public void GetReRollers_ShouldReturnReRollers()
        {
            // Arrange
            var startingRolls = new List<Roll>();
            var reRollers = new List<LobbyPlayer>();
            _startingServiceMock.Setup(service => service.GetReRollers(It.IsAny<List<Roll>>())).Returns(reRollers);

            // Act
            var result = _controller.GetReRollers(startingRolls);

            // Assert
            result.Should().BeOfType<ActionResult<List<LobbyPlayer>>>().Which.Value.Should().BeEquivalentTo(reRollers);
        }

        [Fact]
        public void GetShouldReRoll_ShouldReturnShouldReRoll()
        {
            // Arrange
            var startingRolls = new List<Roll>();
            var shouldReRoll = true;
            _startingServiceMock.Setup(service => service.ShouldReRoll(It.IsAny<List<Roll>>())).Returns(shouldReRoll);

            // Act
            var result = _controller.GetShouldReRoll(startingRolls);

using System.Linq;
            result.Should().BeOfType<ActionResult<bool>>().Which.Value.Should().Be(shouldReRoll);
        }
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoTest.ControllerTests
{
    public class StartingControllerTests
    {
        private readonly Mock<IStartingService> _startingServiceMock;
        private readonly StartingController _controller;

        public StartingControllerTests()
        {
            _startingServiceMock = new Mock<IStartingService>();
            _controller = new StartingController(_startingServiceMock.Object);
        }

        [Fact]
        public void GetStartingRoll_ShouldReturnStartingRoll()
        {
            //Arrange

            //Act

            //Assert

            // Assert
            result.Should().BeOfType<ActionResult<bool>>().Which.Value.Should().Be(shouldReRoll);
        }
    }
}