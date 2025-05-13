using FluentAssertions;
using LudoAPI.Models;
using LudoAPI.Models.Tiles;

namespace LudoTest.Models;

public class TileTest
{

    [Fact]
    public void NextMoveTest()
    {
        //Arrange
        var move = new Move(1,0);
        var tile = new Tile(new Coordinate(0,0), move);
        var blueColor = Color.Blue;
        var piece = new Piece(1, blueColor, tile.Coordinate);
        var expected = piece.Coordinate.CalcNextCoordinateFromMove(move);
        
        //Act
        var actual = tile.NextCoordinate(piece);
        
        //Assert
        actual.Should().BeEquivalentTo(expected);
        
    }
}