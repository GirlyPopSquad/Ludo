using LudoAPI.Models;

namespace LudoAPI.Services
{
    public interface IStartingService
    {
        Lobby HandleRerolls(Lobby lobby);
        List<LobbyPlayer> GetReRollers(List<Roll> startingRolls);
        bool ShouldReRoll(List<Roll> startingRolls);
        Lobby StartingRoll(Lobby lobby);
    }
}
