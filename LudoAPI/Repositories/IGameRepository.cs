using LudoAPI.Models;

namespace LudoAPI.Repositories;

public interface IGameRepository
{
    int Add(Game game);
    Game Get(int id);
    void Update(int id, Game game);
}