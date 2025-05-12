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
            var players = new List<Player> { new Player(1), new Player(2) };
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
            var players = new List<Player> { new Player(1), new Player(2) };
            var lobby = new Lobby(2, players);

            repository.AddNewLobby(players);
            repository.AddNewLobby(players);
            repository.AddNewLobby(players);

            // Act
            var result = repository.Get(2);

            // Assert
            result.Should().BeEquivalentTo(lobby);
        }

        [Fact]
        public void Update_ShouldUpdateLobby()
        {
            // Arrange
            var repository = new LobbyRepository();
            var initialPlayers = new List<Player> { new Player(1), new Player(2) };
            var updatedPlayers = new List<Player> { new Player(1), new Player(2), new Player(3) };
            var initialLobby = repository.AddNewLobby(initialPlayers);
            var updatedLobby = new Lobby(initialLobby.Id, updatedPlayers);
        
            // Act
            repository.UpdateLobby(updatedLobby);
            var result = repository.Get(initialLobby.Id);
        
            // Assert
            result.Should().BeEquivalentTo(updatedLobby);
        }
    }
}
