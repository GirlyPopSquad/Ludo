using LudoAPI.Models;

namespace LudoAPI.Services;

public class StartingRuleService : IStartingRuleService
{
    public int FindStartingPlayer(Lobby lobby)
    {
        if (lobby.Rolls.Count == 0)
        {
            throw new Exception("There are no rolls, in the lobby, those are needed to determine who starts the game");
        }

        var highestRoll = lobby.Rolls.MaxBy(roll => roll.Value);
        
        return highestRoll!.PlayerId;
    }
}