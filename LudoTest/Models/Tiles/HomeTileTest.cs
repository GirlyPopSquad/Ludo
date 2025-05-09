using FluentAssertions;
using LudoAPI.Models;
using LudoAPI.Models.Tiles;

namespace LudoTest.Models.Tiles;

public class HomeTileTest
{

    [Fact]
    public void GetStartCoordinates_ReturnsCorrectCoordinate_ForOneStartingTile()
    {
        // Arrange
        var startTiles = new[]
        {
            new StartTile(new Coordinate(0, 0), Color.Red, new Move(1,0)),
        };
        var homeTile = new HomeTile(new Coordinate(2, 2), Color.Red)
        {
            StartTiles = startTiles
        };

        var expected = startTiles.Select(x => x.Coordinate).ToArray();

        // Act
        var result = homeTile.GetStartCoordinates();

        // Assert
        result.Should().Equal(expected);

    }
}