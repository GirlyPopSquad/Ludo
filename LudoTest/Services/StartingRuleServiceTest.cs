using FluentAssertions;
using LudoAPI.Models;
using LudoAPI.Services;
using LudoTest.Shared;

namespace LudoTest.Services;

public class StartingRuleServiceTest
{
    
    [Fact]
    public void FindStartingPlayer_FindsCorrectPlayer()
    {
        //arrange
        var startingRuleService = new StartingRuleService();
        var expectedPlayer = PlayerTestData.BluePlayer;
        var lobby = new Lobby(1, PlayerTestData.Get4Players());
        var rolls = new List<Roll>
        { new(PlayerTestData.RedPlayer.Id, 4), 
            new(PlayerTestData.GreenPlayer.Id, 2), 
            new(expectedPlayer.Id, 6),
            new(PlayerTestData.YellowPlayer.Id, 1) 
        };
        
        lobby.Rolls = rolls;
        
        //act
        var result = startingRuleService.FindStartingPlayer(lobby);
        
        //assert
        result.Should().Be(expectedPlayer.Id);

    }
}