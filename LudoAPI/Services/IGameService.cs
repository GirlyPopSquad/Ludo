using LudoAPI.Models;

namespace LudoAPI.Services;

public interface IGameService
{
    int CreateFromLobby(int lobbyId);
    int NextTurn(int gameId);
}