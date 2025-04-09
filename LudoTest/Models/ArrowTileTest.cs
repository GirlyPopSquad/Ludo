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
        var arrowTile = new ArrowTile(defaultMove, arrowMove, redColor);
        
        //todo: pieces needs a reference to player/color, for now the color cannot be determined by the piece alone
        var redPiece = new Piece(1);
        var bluePiece = new Piece(2);

        //Act
        var matchingColor = arrowTile.nextMove(redPiece);
        var nonMatchingColor = arrowTile.nextMove(bluePiece);

        //Assert
        Assert.Equal(arrowMove, matchingColor);
        Assert.Equal(defaultMove, nonMatchingColor);
        
    }
}