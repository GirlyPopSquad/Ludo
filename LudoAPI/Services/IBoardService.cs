using LudoAPI.Models;

namespace LudoAPI.Services;

public interface IBoardService
{
    Board MakeBoardFromMap(int gameId, string[,] boardMap);
    Board InitStandardBoard(int i);
}