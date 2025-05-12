using FluentAssertions;
using LudoAPI.Models;
using LudoAPI.Repositories;
using LudoAPI.Services;
using Moq;

namespace LudoTest.Services;

public class RollServiceTest
{

    [Fact]
    public void DoNextRoll_FirstRoll_ReturnsExpectedRoll()
    {
        // Arrange
        var mockRollRepository = new Mock<IRollRepository>();
        var mockGameService = new Mock<IGameService>();
        var mockDiceService = new Mock<IDiceService>();

        var testCurrentPlayer = new Player(1);

        var gameId = 1;

        var diceRoll = 6;

        mockRollRepository
            .Setup(repo => repo.GetRollsFromGame(gameId))
            .Returns((List<Roll>?) null); // Simulate no rolls exist

        mockGameService
            .Setup(service => service.GetCurrentPlayerId(gameId))
            .Returns(testCurrentPlayer.Id);
        mockGameService
            .Setup(service => service.GetIsTimeToRoll(gameId))
            .Returns(true);

        mockDiceService
            .Setup(service => service.RollDice())
            .Returns(diceRoll);

        var rollService = new RollService(mockRollRepository.Object, mockGameService.Object, mockDiceService.Object);
        var expectedRoll = new Roll(testCurrentPlayer.Id, diceRoll);
        
        // Act
        var result = rollService.DoNextRoll(gameId);

        // Assert
        result.Should().NotBeNull()
            .And.Subject.As<Roll>().Should().BeEquivalentTo(expectedRoll);

        mockRollRepository.Verify(repo => repo.SaveRolls(gameId, It.IsAny<List<Roll>>()), Times.Once);

    }
}