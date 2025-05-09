using LudoAPI.Models;

namespace LudoAPI.Services;

public interface IGameService
{
    int CreateFromLobby(int lobbyId);
    Color NextTurn(int gameId);
    Color GetCurrentPlayerId(int gameId);
}