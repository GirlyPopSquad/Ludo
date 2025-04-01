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
            var expectedLobby = new Lobby(1, players);

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
            var lobby = new Lobby(2, players);

            repository.AddNewLobby(players);
            repository.AddNewLobby(players);
            repository.AddNewLobby(players);

            // Act
            var result = repository.Get(2);

            // Assert
            result.Should().BeEquivalentTo(lobby);
        }
    }
}
