using FluentAssertions;
using LudoAPI.Models;
using LudoAPI.Models.Tiles;

namespace LudoTest.Models;

public class ArrowTileTest
{

    [Fact]
    public void NextCoordinate_ShouldReturnExpectedBasedOnTileAndPieceColorMatch()
    {
        //Arrange
        var defaultMove = new Move(1, 0);
        var arrowMove = new Move(0, 1);
        const Color redColor = Color.Red;
        const Color blueColor = Color.Blue;
        var redArrowTile = new ArrowTile(new Coordinate(0,0), redColor, defaultMove, arrowMove);
        var redPiece = new Piece(1, redColor, redArrowTile.Coordinate);
        var bluePiece = new Piece(2, blueColor, redArrowTile.Coordinate);

        var expectedCoordRed = redPiece.Coordinate.CalcNextCoordinateFromMove(arrowMove);
        var expectedCoordBlue = bluePiece.Coordinate.CalcNextCoordinateFromMove(defaultMove);

        //Act
        var actualCoordRed = redArrowTile.NextCoordinate(redPiece);
        var actualCoordBlue = redArrowTile.NextCoordinate(bluePiece);

        //Assert
        actualCoordRed.Should().BeEquivalentTo(expectedCoordRed);
        actualCoordBlue.Should().BeEquivalentTo(expectedCoordBlue);
        
    }
}