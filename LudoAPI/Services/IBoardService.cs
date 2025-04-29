using LudoAPI.Models;

namespace LudoAPI.Services;

public interface IBoardService
{
    Board InitStandardBoard(int gameId);
}