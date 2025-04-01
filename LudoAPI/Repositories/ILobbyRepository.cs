using LudoAPI.Models;

namespace LudoAPI.Repositories;

public interface ILobbyRepository
{
    List<Lobby> Lobbies { get; }
    Lobby AddNewLobby(List<LobbyPlayer> lobbyPlayers);
    Lobby Get(int id);
}