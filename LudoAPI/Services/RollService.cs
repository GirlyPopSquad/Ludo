using LudoAPI.Models;
using LudoAPI.Repositories;

namespace LudoAPI.Services;

public class RollService : IRollService
{
    private readonly IRollRepository _rollRepository;
    private readonly IGameService _gameService;
    private readonly IDiceService _diceService;

    public RollService(IRollRepository rollRepository, IGameService gameService, IDiceService diceService)
    {
        _rollRepository = rollRepository;
        _gameService = gameService;
        _diceService = diceService;
    }

    public Roll DoNextRoll(int gameId)
    {
        var hasGameEnded = _gameService.HasGameEnded(gameId);
        if (hasGameEnded)
        {
            throw new Exception("Game has ended, and no further rolls can be made");
        }
        
        var isValidRoll = _gameService.GetIsTimeToRoll(gameId);

        if (!isValidRoll)
        {
            throw new Exception("This game is not ready for next roll, current Roll might need to be handled first");
        }
        
        var currentPlayer = _gameService.GetCurrentPlayerId(gameId);

        var newRoll = new Roll(currentPlayer, _diceService.RollDice());
        
        _rollRepository.SaveRollToGame(gameId, newRoll);
        _gameService.UpdateIsTimeToRoll(gameId, false);
            
        return newRoll;
    }

    public Roll GetLastestRoll(int gameId)
    {
        return _rollRepository.GetLatestRoll(gameId);
    }

    public List<Roll> GetLatestRolls(int gameId, int limit)
    {
        return _rollRepository.GetLatestRolls(gameId, limit);
    }
}