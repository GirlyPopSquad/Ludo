using LudoAPI.Models;
using LudoAPI.Repositories;

namespace LudoAPI.Services;

public class RollService : IRollService
{
    private IRollRepository _rollRepository;
    private IGameService _gameService;
    private IDiceService _diceService;

    public RollService(IRollRepository rollRepository, IGameService gameService, IDiceService diceService)
    {
        _rollRepository = rollRepository;
        _gameService = gameService;
        _diceService = diceService;
    }

    public Roll DoNextRoll(int gameId)
    {
        var isValidRoll = _gameService.GetIsTimeToRoll(gameId);

        if (!isValidRoll)
        {
            throw new Exception("This game is not ready for next roll, if game exists, current Roll might need to be handled first");
        }

        //if its the first roll of the game, a list is created
        var gameRolls = _rollRepository.GetRollsFromGame(gameId) ?? [];

        var currentPlayer = _gameService.GetCurrentPlayerId(gameId);

        var newRoll = new Roll(currentPlayer, _diceService.RollDice());
            
        _rollRepository.SaveRolls(gameId, gameRolls);
        _gameService.UpdateIsTimeToRoll(gameId, false);
            
        return newRoll;
    }
}