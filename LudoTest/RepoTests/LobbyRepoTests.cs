using LudoAPI.Models;
using LudoAPI.Repositories;
using FluentAssertions;
using LudoTest.Shared;
using LudoAPI.Services;

namespace LudoTest.RepoTests
{
    public class LobbyRepoTests
    {
        private readonly IIdGeneratorService<Lobby> _idGeneratorService;
        public LobbyRepoTests()
        {
            _idGeneratorService = new IdGeneratorService<Lobby>();
        }

        [Fact]
        public void Add_ShouldAddLobbyToList()
        {
            // Arrange
            var repository = new LobbyRepository(_idGeneratorService);
            var players = PlayerTestData.Get4Players();
            var expectedLobby = new Lobby(1, players);

            // Act
            var actualLobby = repository.AddNewLobby(players);

            // Assert
            actualLobby.Should().BeEquivalentTo(expectedLobby);
        }

        [Fact]
        public void Get_ShouldReturnLobbyById()
        {
            // Arrange
            var repository = new LobbyRepository(_idGeneratorService);
            var players = PlayerTestData.Get4Players();
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
            var repository = new LobbyRepository(_idGeneratorService);
            var players = PlayerTestData.Get4Players();
            var lobby = repository.AddNewLobby(players);

            var roll = new Roll(players[0].Id, 6);
            
            lobby.Rolls.Add(roll);
            
            var expectedLobby = new Lobby(lobby.Id, players);
            expectedLobby.Rolls.Add(roll);
        
            // Act
            repository.UpdateLobby(lobby);
            var result = repository.Get(lobby.Id);
        
            // Assert
            result.Should().BeEquivalentTo(expectedLobby);
        }
    }
}
