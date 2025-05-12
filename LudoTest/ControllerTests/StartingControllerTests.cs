using FluentAssertions;
using LudoAPI.Controllers;
using LudoAPI.Models;
using LudoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

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
            // Arrange
            var lobby = new Lobby(1, new List<Player>());
            _startingServiceMock.Setup(s => s.StartingRoll(It.IsAny<Lobby>())).Returns(lobby);

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
            var reRollers = new List<Player> { new Player(1) };
            _startingServiceMock.Setup(service => service.GetReRollers(It.IsAny<List<Roll>>())).Returns(reRollers);

            // Act
            var result = _controller.GetReRollers(startingRolls);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(reRollers);
        }

        [Fact]
        public void GetReRollers_ShouldReturnBadRequest_WhenNoReRollersFound()
        {
            // Arrange
            var startingRolls = new List<Roll>();
            var reRollers = new List<Player>();
            _startingServiceMock.Setup(service => service.GetReRollers(It.IsAny<List<Roll>>())).Returns(reRollers);

            // Act
            var result = _controller.GetReRollers(startingRolls);

            // Assert
            result.Result.Should().BeOfType<BadRequestObjectResult>()
                .Which.Value.Should().Be("Could not find rerollers");
        }

        [Fact]
        public void GetShouldReRoll_ShouldReturnTrue_WhenRerollIsNeeded()
        {
            // Arrange
            var startingRolls = new List<Roll>();
            _startingServiceMock.Setup(service => service.ShouldReRoll(It.IsAny<List<Roll>>())).Returns(true);

            // Act
            var result = _controller.GetShouldReRoll(startingRolls);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().Be(true);
        }

        [Fact]
        public void GetShouldReRoll_ShouldReturnFalse_WhenRerollIsNotNeeded()
        {
            // Arrange
            var startingRolls = new List<Roll>();
            _startingServiceMock.Setup(service => service.ShouldReRoll(It.IsAny<List<Roll>>())).Returns(false);

            // Act
            var result = _controller.GetShouldReRoll(startingRolls);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().Be(false);
        }

        [Fact]
        public void GetShouldReRoll_ShouldReturnBadRequest_WhenExceptionOccurs()
        {
            // Arrange
            var startingRolls = new List<Roll>();
            _startingServiceMock.Setup(service => service.ShouldReRoll(It.IsAny<List<Roll>>()))
                .Throws(new Exception("Some error"));

            // Act
            var result = _controller.GetShouldReRoll(startingRolls);

            // Assert
            result.Result.Should().BeOfType<BadRequestObjectResult>()
                .Which.Value.Should().Be("Could not determine if reroll is needed");
        }
    }
}