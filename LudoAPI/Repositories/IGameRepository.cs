using LudoAPI.Models;

namespace LudoAPI.Repositories;

public interface IGameRepository
{
    Game NewGame(int startingPlayerId, List<Player> players);
}