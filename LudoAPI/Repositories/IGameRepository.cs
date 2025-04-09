using LudoAPI.Models;

namespace LudoAPI.Repositories;

public interface IGameRepository
{
    Game NewGame(int currentPlayerId, List<Player> players);
}