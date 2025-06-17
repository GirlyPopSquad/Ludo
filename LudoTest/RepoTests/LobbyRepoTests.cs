using LudoAPI.Models;
using LudoAPI.Repositories;
using FluentAssertions;
using LudoTest.Shared;
using LudoAPI.Services;

namespace LudoTest.RepoTests
{
    public class LobbyRepoTests
    {
        [Fact]
        public void Save_AddsLobbyToDictionary_IfSuccess()
        {
            // Arrange
            var repository = new LobbyRepository();
            var players = PlayerTestData.Get4Players();
            var expectedLobby = new Lobby(1, players);

            // Act
            repository.Save(expectedLobby);

            // Assert
            repository.GetLobbies().Should().ContainKey(expectedLobby.Id);
        }

        [Fact]
        public void Get_ShouldReturnLobbyById()
        {
            // Arrange
            var repository = new LobbyRepository();
            var players = PlayerTestData.Get4Players();
            var lobby = new Lobby(2, players);

            repository.Save(lobby);
            repository.Save(lobby);
            repository.Save(lobby);

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
            var players = PlayerTestData.Get4Players();
            var expectedLobby = new Lobby(2, players);
            repository.Save(expectedLobby);

            var roll = new Roll(players[0].Id, 6);
            
            expectedLobby.Rolls.Add(roll);
        
            // Act
            repository.Update(expectedLobby);
            var result = repository.Get(expectedLobby.Id);
        
            // Assert
            result.Should().BeEquivalentTo(expectedLobby);
        }
    }
}
