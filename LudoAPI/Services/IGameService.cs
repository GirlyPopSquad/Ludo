using LudoAPI.Models;

namespace LudoAPI.Services;

public interface IGameService
{
    public Game NewGame(Lobby lobby);
    public LobbyPlayer HaveTurn(Game game, LobbyPlayer player);
}