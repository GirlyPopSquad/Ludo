using LudoAPI.Models;

namespace LudoTest.Models;

public class PlayerTest
{

    //todo: make theory, so all colors can be tested
    [Fact]
    public void player_getsIdFromColor()
    {
        //Arrange
        var color = Color.Yellow;
        var player = new Player(color);
            
        //Act
        var intFromColor = (int) color;
            
        //Assert
        Assert.Equal(intFromColor, player.Id);
    }
}