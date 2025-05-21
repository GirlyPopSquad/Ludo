using LudoAPI.Models;

namespace LudoAPI.Repositories;

public interface IRollRepository
{
    void SaveRollToGame(int gameId, Roll roll);
    List<Roll>? GetRollsFromGame(int gameId);
    Roll GetLatestRoll(int gameId);
    List<Roll> GetLatestRolls(int gameId, int limit);
}