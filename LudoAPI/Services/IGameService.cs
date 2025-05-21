namespace LudoAPI.Services;

public interface IGameService
{
    int CreateFromLobby(int lobbyId);
    void NextTurn(int gameId);
    int GetCurrentPlayerId(int gameId);
    bool GetIsTimeToRoll(int gameId);
    void UpdateIsTimeToRoll(int gameId, bool isTimeToRoll);
}