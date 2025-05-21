using LudoAPI.Models;

namespace LudoAPI.Services
{
    public interface IStartingService
    {
        List<Player> GetReRollers(List<Roll> startingRolls);
        bool ShouldReRoll(List<Roll> startingRolls);
        Lobby StartingRoll(Lobby lobby);
        Lobby HandleReroll(int lobbyId, Player player);
        void RemoveOldRolls(int id, List<Player> rerollers);
    }
}
