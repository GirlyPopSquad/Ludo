﻿using LudoAPI.Models;

namespace LudoTest.Models;

public class TileTest
{

    [Fact]
    public void NextMoveTest()
    {
        //Arrange
        var move = new Move(1,0);
        var tile = new Tile(move);
        var blueColor = Color.Blue;
        var piece = new Piece(blueColor);
        
        //Act
        var actual = tile.nextMove(piece);
        
        //Assert
        Assert.Equal(actual, move);
        
    }
}