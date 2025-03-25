using FluentAssertions;
using LudoAPI.Controllers;
using LudoAPI.Models;
using LudoAPI.Services;
using Moq;

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
            var initialPlayers = new List<LobbyPlayer>
            {
                new LobbyPlayer(1),
                new LobbyPlayer(2),
                new LobbyPlayer(3),
                new LobbyPlayer(4)
            };
            
            var initialLobby = new Lobby(initialPlayers, 1);
            
            var startingRoll = new Roll(new LobbyPlayer(1), 6);
            var expectedLobby = new Lobby(initialPlayers, 1);
            expectedLobby.StartingRolls.Add(startingRoll);
            
            
            _startingServiceMock.Setup(service => service.StartingRoll(initialLobby)).Returns(expectedLobby);

            // Act
            var result = _controller.GetStartingRoll(initialLobby);
            // Assert
            result.Should().NotBeNull();
            result.Value.Should().Be(expectedLobby);

        }
    }
}
