using FluentAssertions;
using LudoAPI.Models;
using LudoAPI.Repositories;
using LudoAPI.Services;
using Moq;

namespace LudoTest.GameServiceTests
{
    public class GameServiceTests
    {
        
        private readonly Mock<IGameRepository> _gameRepository = new Mock<IGameRepository>();
        
        [Theory]
        [ClassData(typeof(LobbyTestData))]
        public void StartNewGame_ShouldReturnANewGame(Lobby lobby)
        {
            //Arrange
            var startingRolls = new List<Roll>
            {
                new Roll(lobby.Players[0], 4),
                new Roll(lobby.Players[1], 1),
                new Roll(lobby.Players[2], 6),
                new Roll(lobby.Players[3], 4),
            };
            lobby.Rolls = startingRolls;
            
            var expectedStartingPlayerId = lobby.Players[2].Id;
            var pieces = new List<Piece>
            {
                new Piece(1),
                new Piece(2),
                new Piece(3),
                new Piece(4),
            };
            var players = lobby.Players.Select(player => new Player(player.Id, pieces)).ToList();
            var game = new Game(1, players, expectedStartingPlayerId);
            
            _gameRepository.Setup(gr => gr.NewGame(expectedStartingPlayerId, It.IsAny<List<Player>>())).Returns(game);
            
            GameService service = new GameService(_gameRepository.Object);

            //Act
            var newGame = service.NewGame(lobby);

            //Assert
            newGame.CurrentPlayerId.Should().Be(expectedStartingPlayerId);
            newGame.Players.Count.Should().Be(lobby.Players.Count);
            newGame.Should().BeEquivalentTo(game);
        }
    }
}
