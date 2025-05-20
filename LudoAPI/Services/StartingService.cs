using LudoAPI.Models;

namespace LudoAPI.Services;

public class StartingService : IStartingService
{
    private readonly IDiceService _diceService;
    private readonly ILobbyService _lobbyService;

    public StartingService(IDiceService diceService, ILobbyService lobbyService)
    {
        _diceService = diceService;
        _lobbyService = lobbyService;
    }

    public Lobby HandleReroll(int lobbyId, int playerId)
    {
        var lobby = _lobbyService.GetLobbyById(lobbyId);

        var oldRoll = lobby.Rolls.Find(r => r.PlayerId == playerId);

        if (oldRoll == null)
        {
            throw new Exception("This player hasn't rolled yet, and therefore cant reroll");
        }

        var newRollValue = _diceService.RollDice();
        oldRoll.Value = newRollValue;

        _lobbyService.UpdateLobby(lobby);
        return lobby;
    }

    public List<Player> GetReRollers(int lobbyId)
    {
        var lobby = _lobbyService.GetLobbyById(lobbyId);

        var startingRolls = lobby.Rolls;
        var highestRoll = startingRolls.Max(x => x.Value);

        var rerollersIds = startingRolls
            .Where(r => r.Value == highestRoll)
            .Select(roll => roll.PlayerId)
            .ToList();

        var rerollers = lobby.Players
            .IntersectBy(rerollersIds, player => player.Id)
            .ToList();

        //remove non-rerollers rolls
        lobby.Rolls = startingRolls
            .IntersectBy(rerollersIds, roll => roll.PlayerId)
            .ToList();

        _lobbyService.UpdateLobby(lobby);

        return rerollers;
    }

    public bool ShouldReRoll(List<Roll> startingRolls)
    {
        int highest = startingRolls.Max(x => x.Value);
        return startingRolls.Count(x => x.Value == highest) > 1;
    }

    public Lobby DoNextStartingRoll(int lobbyId)
    {
        var lobby = _lobbyService.GetLobbyById(lobbyId);

        int playerCount = lobby.Players.Count;

        int rollCount = lobby.Rolls.Count;

        if (rollCount >= playerCount)
        {
            throw new Exception("All players have already rolled");
        }

        var player = lobby.Players[rollCount];
        var value = _diceService.RollDice();

        var newRoll = new Roll(player.Id, value);
        lobby.Rolls.Add(newRoll);

        _lobbyService.UpdateLobby(lobby);

        return lobby;
    }
}