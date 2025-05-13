using LudoAPI.Models;

namespace LudoAPI.Repositories;

public class RollRepository : IRollRepository
{
    //gameId and corresponding pieces
    private readonly Dictionary<int, List<Roll>> _rolls = new();

    public void SaveRollToGame(int gameId, Roll roll)
    {
        //if no list exists, one is created
        if (!_rolls.ContainsKey(gameId))
        {
            _rolls.Add(gameId, []);
        }
        _rolls[gameId].Add(roll);
    }

    public List<Roll>? GetRollsFromGame(int gameId)
    {
        //todo: could also return an empty list, to better mimic database
        return _rolls.GetValueOrDefault(gameId);
    }

    public Roll GetLatestRoll(int gameId)
    {
        var rolls = _rolls[gameId];
        var result = rolls.Last();
        return result;
    }

    public List<Roll> GetLatestRolls(int gameId, int limit)
    {
        var rolls = _rolls[gameId];
        if (rolls.Count <= limit)
        {
            return rolls;
        }
        
        var result =  rolls.GetRange(rolls.Count - limit, limit);
        return result;
    }
}