using FluentAssertions;
using LudoAPI.Models;
using LudoAPI.Repositories;

namespace LudoTest.Repositories;

public class GameRepositoryTest
{
    private readonly GameRepository _gameRepository = new GameRepository();

    [Fact]
    public void Add_WorksAsExpected()
    {
        //Arrange
        var startingPlayerId = 1;
        var testPlayer1 = new GamePlayer(startingPlayerId);

        var testGame =
            new Game(new List<GamePlayer>() { testPlayer1, new GamePlayer(2), new GamePlayer(3), new GamePlayer(4) },
                startingPlayerId);

        //Act
        var id = _gameRepository.Add(testGame);

        //Assert
        var expectedGame = new Game(1, testGame);
        
        _gameRepository.Get(id).Should().BeEquivalentTo(expectedGame);
    }
}