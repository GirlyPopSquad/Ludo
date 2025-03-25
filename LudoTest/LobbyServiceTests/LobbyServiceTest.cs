using FluentAssertions;
using LudoAPI.Models;
using LudoAPI.Repositories;
using LudoAPI.Services;
using Moq;

namespace LudoTest.Services;

public class LobbyServiceTest
{

    [Fact]
    public void CreateLobby()
    {

      var lobbyRepo = new Mock<LobbyRepository>();

        //Arrange
        var lobbyService = new LobbyService(lobbyRepo.Object);
        
        var players = new List<LobbyPlayer>()
        {
            new LobbyPlayer(1), new LobbyPlayer(2), new LobbyPlayer(3), new LobbyPlayer(4)
        };
        
        var expected = new Lobby(players,1);
        
        //Act
        var actual = lobbyService.CreateLobby();
        
        //Assert
        expected.Should().BeEquivalentTo(actual);
    }
}