﻿using FluentAssertions;
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
            var lobby = new Lobby(new List<LobbyPlayer>(), 1);
            _startingServiceMock.Setup(s => s.StartingRoll(It.IsAny<Lobby>())).Returns(lobby);

            // Act
            var result = _controller.GetStartingRoll(lobby);

            // Assert
            var actionResult = result.Should().BeOfType<ActionResult<Lobby>>().Subject;
            var returnValue = actionResult.Value.Should().BeOfType<Lobby>().Subject;
            returnValue.Should().BeEquivalentTo(lobby);
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
            var actionResult = result.Should().BeOfType<ActionResult<List<LobbyPlayer>>>().Subject;
            var returnValue = actionResult.Value.Should().BeOfType<List<LobbyPlayer>>().Subject;
            returnValue.Should().BeEquivalentTo(reRollers);
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

            // Assert
            var actionResult = result.Should().BeOfType<ActionResult<bool>>().Subject;
            var returnValue = actionResult.Value;
            returnValue.Should().Be(shouldReRoll);
        }
    }
}