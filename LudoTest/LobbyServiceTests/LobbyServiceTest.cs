using FluentAssertions;
using LudoAPI.Models;
using LudoAPI.Repositories;
using LudoAPI.Services;
using LudoTest.Shared;
using Moq;

namespace LudoTest.LobbyServiceTests;

public class LobbyServiceTest
{
    private readonly Mock<ILobbyRepository> _repositoryMock;
    private readonly Mock<IPlayerGenerator> _playerGenerator;
    private readonly IIdGeneratorService<Lobby> _idGeneratorService;
    public LobbyServiceTest()
    {
        _repositoryMock = new Mock<ILobbyRepository>();
        _playerGenerator = new Mock<IPlayerGenerator>();
        _idGeneratorService = new IdGeneratorService<Lobby>();
    }

    [Fact]
    public void CreateLobby_ShouldCreateAndReturnNewLobby_IfSuccess()
    {
        //Arrange
        var lobbyPlayers = PlayerTestData.Get4Players();
        
        var expectedLobby = new Lobby(1, lobbyPlayers);

        _repositoryMock
           .Setup(p => p.GetLobbies())
           .Returns(new Dictionary<int, Lobby>());

        _playerGenerator
           .Setup(p => p.GeneratePlayers())
           .Returns(new List<Player>
           {
                new ((Color)1),
                new ((Color)2),
                new ((Color)3),
                new ((Color)4),
           });

        var lobbyService = new LobbyService(_repositoryMock.Object, _playerGenerator.Object, _idGeneratorService);
        
        //Act
        var actualLobby = lobbyService.CreateLobby();
        
        //Assert
        actualLobby.Should().BeEquivalentTo(expectedLobby);
    }

    [Fact]
    public void GetLobbyById()
    {
        //Arrange
        var expectedLobby = new Lobby(1, PlayerTestData.Get4Players());
        
        _repositoryMock.Setup(lobbyRepo => lobbyRepo.Get(1)).Returns(expectedLobby);
        
        var lobbyService = new LobbyService(_repositoryMock.Object, _playerGenerator.Object, _idGeneratorService);
        
        //Act
        var actualLobby = lobbyService.GetLobbyById(1);
        
        //Assert
        actualLobby.Should().BeEquivalentTo(expectedLobby);
    }

    [Fact]
    public void UpdateLobby()
    {
        //Arrange
        var testLobby = new Lobby(1, PlayerTestData.Get4Players());
        
        var lobbyService = new LobbyService(_repositoryMock.Object, _playerGenerator.Object, _idGeneratorService);
        //Act
        lobbyService.UpdateLobby(testLobby);
        
        //Assert
        _repositoryMock.Verify(repo => repo.Update(testLobby), Times.Once);
    }
}