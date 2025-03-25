using LudoAPI.Models;

namespace LudoAPI.Services;

public interface IGameService
{
    public Game Start(Lobby lobby);
    public LobbyPlayer HaveTurn(Game game, LobbyPlayer player);
}