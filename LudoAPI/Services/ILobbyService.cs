using LudoAPI.Models;

namespace LudoAPI.Services;

public interface ILobbyService
{
    Lobby CreateLobby();
    Lobby GetLobbyById(int id);
    void UpdateLobby(Lobby lobby);
    void RemoveOldRolls(int id, List<Player> rerollers);
}