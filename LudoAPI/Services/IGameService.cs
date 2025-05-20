using LudoAPI.Models;

namespace LudoAPI.Services;

public interface IGameService
{
    void NextTurn(int gameId);
    int GetCurrentPlayerId(int gameId);
    bool GetIsTimeToRoll(int gameId);
    void UpdateIsTimeToRoll(int gameId, bool isTimeToRoll);
    void HandlePlayerFinished(int gameId, Color pieceColor);
    bool HasPlayerFinished(int gameId, int playerId);
    bool HasGameEnded(int gameId);
    int CreateGame(Game game);
}