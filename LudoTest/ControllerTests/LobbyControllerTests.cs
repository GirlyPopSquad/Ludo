using FluentAssertions;
using LudoAPI.Controllers;
using LudoAPI.Models;
using LudoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoTest.ControllerTests
{
    public class LobbyControllerTests
    {
        private readonly Mock<ILobbyService> _lobbyServiceMock;
        private readonly LobbyController _controller;

        public LobbyControllerTests()
        {
            _lobbyServiceMock = new Mock<ILobbyService>();
            _controller = new LobbyController(_lobbyServiceMock.Object);
        }

        [Fact]
        public void CreateLobby_ShouldReturnNewLobby()
        {
            //Arrange
            var expectedLobby = new Lobby(new List<LobbyPlayer>(), 1);
            _lobbyServiceMock.Setup(service => service.CreateLobby())
                .Returns(expectedLobby);

            //Act
            var result = _controller.CreateLobby();

            //Assert
            result.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeOfType<Lobby>()
                .And.BeEquivalentTo(expectedLobby);
        }

        [Fact]
        public void CreateLobby_ShouldReturnBadRequest_WhenLobbyIsNull()
        {
            // Arrange
            _lobbyServiceMock.Setup(s => s.CreateLobby()).Returns((Lobby)null);

            // Act
            var result = _controller.CreateLobby();

            // Assert
            result.Result.Should().BeOfType<BadRequestObjectResult>()
                .Which.Value.Should().Be("Lobby could not be created");
        }
    }
}
