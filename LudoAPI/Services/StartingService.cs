using LudoAPI.Models;
using System.Collections.Concurrent;

namespace LudoAPI.Services
{
    public class StartingService : IStartingService
    {
        private readonly IPlayerService _playerService;
        private readonly IDiceService _diceService;

        public StartingService(IDiceService diceService)
        {
            _diceService = diceService;
        }

        public Lobby HandleRerolls(Lobby lobby)
        {
            throw new NotImplementedException();
        }

        public List<LobbyPlayer> GetReRollers(List<Roll> startingRolls)
        {
            int highest = startingRolls.Max(x => x.Value);
            return startingRolls.Where(x => x.Value == highest)
                .Select(x => x.Player).ToList();
        }

        public bool ShouldReRoll(List<Roll> startingRolls)
        {
            int highest = startingRolls.Max(x => x.Value);
            return startingRolls.Count(x => x.Value == highest) > 1;
        }

        public Lobby StartingRoll(Lobby lobby)
        {
            int playerCount = lobby.Players.Count();

            int rollCount = lobby.StartingRolls.Count();

            if (rollCount >= playerCount)
            {
                throw new Exception("All players have already rolled");
            }

            var player = lobby.Players[rollCount];
            var value = _diceService.RollDice();

            var newRoll = new Roll(player, value);
            lobby.StartingRolls.Add(newRoll);

            return lobby;
        }
    }
}
