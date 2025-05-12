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
        var testPlayer1 = new Player(startingPlayerId);

        var testGame =
            new Game(new List<Player>() { testPlayer1, new Player(2), new Player(3), new Player(4) },
                startingPlayerId);

        //Act
        var id = _gameRepository.Add(testGame);

        //Assert
        var expectedGame = new Game(1, testGame);
        
        _gameRepository.Get(id).Should().BeEquivalentTo(expectedGame);
    }
}