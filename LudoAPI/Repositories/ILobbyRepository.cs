using LudoAPI.Models;

namespace LudoAPI.Repositories;

public interface ILobbyRepository
{
    Lobby AddNewLobby(List<LobbyPlayer> lobbyPlayers);
    Lobby Get(int id);
    void UpdateLobby(Lobby lobby);
    void RemoveOldRolls(int lobbyId, List<LobbyPlayer> rerollers);
}