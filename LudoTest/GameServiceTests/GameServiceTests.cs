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
        public void StartNewGame(Lobby lobby)
        {
            //Arrange
            GameService service = new GameService();

            //Act
            var newGame = service.Start(lobby);

            //Assert

            //A starting player hasn't yet been decided
            newGame.currentPlayerId.Should().BeNull();
            newGame.players.Count.Should().Be(4);
            newGame.players.Should().BeEquivalentTo(lobby.Players);
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
