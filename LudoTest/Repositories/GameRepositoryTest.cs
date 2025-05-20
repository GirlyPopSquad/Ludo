using FluentAssertions;
using LudoAPI.Models;
using LudoAPI.Repositories;
using LudoTest.Shared;

namespace LudoTest.Repositories;

public class GameRepositoryTest
{
    private readonly GameRepository _gameRepository = new();

    [Fact]
    public void Add_WorksAsExpected()
    {
        //Arrange
        var redPlayer = PlayerTestData.RedPlayer;

        var testGame =
            new Game([redPlayer, PlayerTestData.BluePlayer, PlayerTestData.GreenPlayer, PlayerTestData.YellowPlayer],
                redPlayer.Id);

        //Act
        var id = _gameRepository.Add(testGame);

        //Assert
        var expectedGame = new Game(1, testGame);
        
        _gameRepository.Get(id).Should().BeEquivalentTo(expectedGame);
    }
}