using LudoAPI.Models;

namespace LudoAPI.Repositories;

public interface IBoardRepository
{
    int Add(Board board);
    Board Get(int id);
    Board GetByGameId(int gameId);
}