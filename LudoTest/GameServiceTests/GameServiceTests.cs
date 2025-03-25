using FluentAssertions;
using LudoAPI.Models;
using LudoAPI.Services;
using Moq;

namespace LudoTest.GameServiceTests
{
    public class GameServiceTests
    {

        [Fact]
        public void GameService_NewTurn_BackToQueue()
        {
            //Arrange
            GameService service = new GameService();

            //Act
            service.NewTurn();

            //Assert
            //queueService.Players.Should().Contain(player);
        }

        [Fact]
        public void StartNewGame()
        {
            GameService service = new GameService();
            //Arrange 
            var bluePlayer = new LobbyPlayer(1);
            var redPlayer = new LobbyPlayer(2);
            var yellowPlayer = new LobbyPlayer(3);
            var greenPlayer = new LobbyPlayer(4);
            var players = new List<LobbyPlayer> { bluePlayer, redPlayer, yellowPlayer, greenPlayer };

            var lobby = new Lobby(players, 1);

            //Act
            var newGame = service.Start(lobby);

            //Assert

            //A starting player hasn't yet been decided
            newGame.currentPlayerId.Should().BeNull();

            newGame.players.Count.Should().Be(4);
            newGame.players.Should().BeEquivalentTo(players);

        }
    }
}
