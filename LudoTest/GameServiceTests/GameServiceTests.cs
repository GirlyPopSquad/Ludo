using System.Collections;
using FluentAssertions;
using LudoAPI.Models;
using LudoAPI.Services;

namespace LudoTest.GameServiceTests
{
    public class GameServiceTests
    {

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
            
            GameService service = new GameService();

            //Act
            var newGame = service.Start(lobby);

            //Assert
            newGame.CurrentPlayerId.Should().Be(expectedStartingPlayerId);
            newGame.Players.Count.Should().Be(lobby.Players.Count);
        }
    }
    
    public class LobbyTestData : IEnumerable<object[]>
    {
        private readonly List<object[]> _data =
        [
            new object[]
            {
                new Lobby(1, [
                    new LobbyPlayer(1),
                    new LobbyPlayer(2),
                    new LobbyPlayer(3),
                    new LobbyPlayer(4)
                ])
            }
        ];

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
