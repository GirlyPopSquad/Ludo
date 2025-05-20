using LudoAPI.Models;

namespace LudoAPI.Repositories;

public interface ILobbyRepository
{
    Lobby AddNewLobby(List<Player> lobbyPlayers);
    Lobby Get(int id);
    void UpdateLobby(Lobby lobby);
    void Remove(int lobbyId);
}