using LudoAPI.Models;

namespace LudoTest.Models;

public class ArrowTileTest
{

    [Fact]
    public void NextMoveTest()
    {
        //Arrange
        var defaultMove = new Move(1, 0);
        var arrowMove = new Move(0, 1);
        var redColor = Color.Red;
        var blueColor = Color.Blue;
        var arrowTile = new ArrowTile(new Coordinate(0,0), redColor, defaultMove, arrowMove);
        var redPiece = new Piece(redColor);
        var bluePiece = new Piece(blueColor);

        //Act
        var matchingColor = arrowTile.NextMove(redPiece);
        var nonMatchingColor = arrowTile.NextMove(bluePiece);

        //Assert
        Assert.Equal(arrowMove, matchingColor);
        Assert.Equal(defaultMove, nonMatchingColor);
        
    }
}