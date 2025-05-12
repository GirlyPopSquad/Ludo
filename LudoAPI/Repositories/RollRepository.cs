using LudoAPI.Models;

namespace LudoAPI.Repositories;

public class RollRepository : IRollRepository
{
    //gameId and corresponding pieces
    private Dictionary<int, List<Roll>> _rolls = new();

    public void SaveRolls(int gameId, List<Roll> rolls)
    {
        _rolls.Add(gameId, rolls);
    }

    public void SaveRollToGame(int gameId, Roll roll)
    {
        _rolls[gameId].Add(roll);
    }

    public List<Roll>? GetRollsFromGame(int gameId)
    {
        //todo: could also return an empty list, to better mimic database
        return _rolls.GetValueOrDefault(gameId);
    }
    
}