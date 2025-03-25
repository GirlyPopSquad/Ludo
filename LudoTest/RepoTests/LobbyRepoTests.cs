using LudoAPI.Models;
using LudoAPI.Repositories;
using FluentAssertions;
using Xunit;

namespace LudoTest.RepoTests
{
    public class LobbyRepoTests
    {
        [Fact]
        public void Add_ShouldAddLobbyToList()
        {
            // Arrange
            var repository = new LobbyRepository();
            var players = new List<LobbyPlayer> { new LobbyPlayer(1), new LobbyPlayer(2) };
            var expectedLobby = new Lobby(players, 1);

            // Act
            var actualLobby = repository.AddNewLobby(players);

            // Assert
            repository.Lobbies.Should().ContainEquivalentOf(expectedLobby);
            actualLobby.Should().BeEquivalentTo(expectedLobby);
            repository.Lobbies.Count.Should().Be(1);
        }

        [Fact]
        public void Get_ShouldReturnLobbyById()
        {
            // Arrange
            var repository = new LobbyRepository();
            var players = new List<LobbyPlayer> { new LobbyPlayer(1), new LobbyPlayer(2) };
            var lobby = new Lobby(players, 2);

            repository.AddNewLobby(players);
            repository.AddNewLobby(players);
            repository.AddNewLobby(players);

            // Act
            var result = repository.Get(2);

            // Assert
            result.Should().BeEquivalentTo(lobby);
        }

        //[Fact]
        //public void GetNextId_ShouldReturnNextId()
        //{
        //    // Arrange
        //    var repository = new LobbyRepository();
        //    var players1 = new List<LobbyPlayer> { new LobbyPlayer(1), new LobbyPlayer(2) };
        //    var lobby1 = new Lobby(players1, 1);
        //    repository.AddNewLobby(players1);

        //    var players2 = new List<LobbyPlayer> { new LobbyPlayer(3), new LobbyPlayer(4) };
        //    var lobby2 = new Lobby(players2, 2);
        //    repository.AddNewLobby(players2);

        //    // Act
        //    var nextId = repository.GetNextId();

        //    // Assert
        //    nextId.Should().Be(3);
        //}
    }
}
