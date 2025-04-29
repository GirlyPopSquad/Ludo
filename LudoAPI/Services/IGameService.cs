using LudoAPI.Models;

namespace LudoAPI.Services;

public interface IGameService
{
    public int CreateFromLobby(int lobbyId);
    public LobbyPlayer HaveTurn(Game game, LobbyPlayer player);
}