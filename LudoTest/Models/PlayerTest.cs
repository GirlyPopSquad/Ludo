using LudoAPI.Models;

namespace LudoTest.Models;

public class PlayerTest
{

    [Fact]
    public void player_hasCorrectColor()
    {
        //Arrange
        var player = new Player(1);
            
        //Act
        var playerColor = (Color) player.Id;
            
        //Assert
        Assert.Equal(Color.Blue, playerColor);
    }
}