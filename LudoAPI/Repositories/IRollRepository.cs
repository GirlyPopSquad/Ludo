using LudoAPI.Models;

namespace LudoAPI.Repositories;

public interface IRollRepository
{
    void SaveRolls(int gameId, List<Roll> rolls);
    void SaveRollToGame(int gameId, Roll roll);
    List<Roll>? GetRollsFromGame(int gameId);
}