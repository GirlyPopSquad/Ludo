using FluentAssertions;
using JetBrains.Annotations;
using LudoAPI.Controllers;
using LudoAPI.Models;
using LudoAPI.Services;
using LudoTest.GameServiceTests;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LudoTest.Controllers;

[TestSubject(typeof(GameController))]
public class GameControllerTest
{
    private readonly Mock<IGameService> _gameServiceMock = new Mock<IGameService>();

    [Theory]
    [ClassData(typeof(LobbyTestData))]
    public void startGame_ShouldReturnGame(Lobby lobby)
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
        var pieces = new List<Piece>
        {
            new Piece(1),
            new Piece(2),
            new Piece(3),
            new Piece(4),
        };
        var players = lobby.Players.Select(player => new Player(player.Id, pieces)).ToList();
        var game = new Game(1, players, expectedStartingPlayerId);
            
        _gameServiceMock.Setup(gr => gr.NewGame(lobby)).Returns(game);
            
        GameController controller = new GameController(_gameServiceMock.Object);

        //Act
        var result = controller.StartGame(lobby);

        //Assert
        result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeEquivalentTo(game);
    }
}