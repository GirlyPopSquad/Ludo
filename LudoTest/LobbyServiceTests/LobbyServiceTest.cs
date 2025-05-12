using FluentAssertions;
using LudoAPI.Models;
using LudoAPI.Repositories;
using LudoAPI.Services;
using Moq;

namespace LudoTest.LobbyServiceTests;

public class LobbyServiceTest
{
    
    private readonly Mock<ILobbyRepository> _repository = new Mock<ILobbyRepository>();
    
    [Fact]
    public void CreateLobby()
    {
        //Arrange
        var lobbyPlayers = new List<Player>()
        {
            new Player(1),
            new Player(2),
            new Player(3),
            new Player(4),
        };
        
        var expectedLobby = new Lobby(1, lobbyPlayers);
        
        //this disregards the content of the input list, as long as it is a List of LobbyPlayers 
        _repository.Setup(r => r.AddNewLobby(It.IsAny<List<Player>>())).Returns(expectedLobby);
        var lobbyService = new LobbyService(_repository.Object);
        
        //Act
        var actualLobby = lobbyService.CreateLobby();
        
        //Assert
        actualLobby.Should().BeEquivalentTo(expectedLobby);
    }

    [Fact]
    public void GetLobbyById()
    {
        //Arrange
        var expectedLobby = new Lobby(1, new List<Player>()
        {
            new Player(1),
            new Player(2),
            new Player(3),
            new Player(4),
        });
        
        _repository.Setup(lobbyRepo => lobbyRepo.Get(1)).Returns(expectedLobby);
        
        var lobbyService = new LobbyService(_repository.Object);
        
        //Act
        var actualLobby = lobbyService.GetLobbyById(1);
        
        //Assert
        actualLobby.Should().BeEquivalentTo(expectedLobby);
    }

    [Fact]
    public void UpdateLobby()
    {
        //Arrange
        var testLobby = new Lobby(1, new List<Player>
        {
            new Player(1),
            new Player(2),
            new Player(3),
            new Player(4)
        });
        
        _repository.Setup(repo => repo.UpdateLobby(testLobby));
        
        var lobbyService = new LobbyService(_repository.Object);
        //Act
        lobbyService.UpdateLobby(testLobby);
        
        //Assert
        _repository.Verify(repo => repo.UpdateLobby(testLobby), Times.Once);
    }
}