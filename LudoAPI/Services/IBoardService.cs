using LudoAPI.Models;

namespace LudoAPI.Services;

public interface IBoardService
{
    int InitStandardBoard(int gameId);
    Board GetBoard(int boardId);
    Board GetBoardFromGameId(int gameId);
}