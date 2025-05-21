using LudoAPI.Models;

namespace LudoAPI.Services;

public interface IStartingService
{
    List<Player> GetReRollers(int lobbyId);
    bool ShouldReRoll(List<Roll> startingRolls);
    Lobby DoNextStartingRoll(int lobbyId);
    Lobby HandleReroll(int lobbyId, int playerId);
}