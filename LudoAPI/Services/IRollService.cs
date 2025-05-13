using LudoAPI.Models;

namespace LudoAPI.Services;

public interface IRollService
{
    Roll DoNextRoll(int gameId);
    Roll GetLastestRoll(int gameId);
    List<Roll> GetLatestRolls(int gameId, int limit);
}