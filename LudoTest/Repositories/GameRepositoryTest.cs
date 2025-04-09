using FluentAssertions;
using LudoAPI.Models;
using LudoAPI.Repositories;

namespace LudoTest.Repositories;

public class GameRepositoryTest
{

    [Fact]
    public void NewGame_ShouldCreateNewGame()
    {
        // Arrange
        var repository = new GameRepository();
        var players = new List<Player>
        {
            new Player(1, [
                new Piece(1),
                new Piece(2),
                new Piece(3),
                new Piece(4)
            ]),
            new Player(2, [
                new Piece(1),
                new Piece(2),
                new Piece(3),
                new Piece(4)
            ]),
            new Player(3, [
                new Piece(1),
                new Piece(2),
                new Piece(3),
                new Piece(4)
            ]),
            new Player(4, [
                new Piece(1),
                new Piece(2),
                new Piece(3),
                new Piece(4)
            ]),
        };

        var startingPlayerId = 1;
        
        var expectedGame = new Game(1,players, startingPlayerId);
        
        // Act
        var actualGame = repository.NewGame(startingPlayerId, players);
        
        // Assert
        actualGame.Should().BeEquivalentTo(expectedGame);
    }
}