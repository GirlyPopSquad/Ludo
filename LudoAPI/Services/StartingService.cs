using LudoAPI.Models;
using LudoAPI.Repositories;

namespace LudoAPI.Services
{
    public class StartingService : IStartingService
    {
        private readonly IDiceService _diceService;
        private readonly ILobbyService _lobbyService;

        public StartingService(IDiceService diceService, ILobbyService lobbyService)
        {
            _diceService = diceService;
            _lobbyService = lobbyService;
        }

        public Lobby HandleReroll(int lobbyId, Player player)
        {
            var lobby = _lobbyService.GetLobbyById(lobbyId);
            var newRollValue = _diceService.RollDice();

            var oldRoll = lobby.Rolls.Find(r => r.Player.Id == player.Id);

            if (oldRoll == null)
            {
                throw new Exception("This player hasn't rolled yet, and therefore cant reroll");
            }
            
            oldRoll.Value = newRollValue;
            
            _lobbyService.UpdateLobby(lobby);
            return lobby;
        }

        public List<Player> GetReRollers(List<Roll> startingRolls)
        {
            int highest = startingRolls.Max(x => x.Value);
            return startingRolls.Where(x => x.Value == highest)
                .Select(x => x.Player).ToList();
        }

        public void RemoveOldRolls(int id, List<Player> rerollers)
        {
            _lobbyService.RemoveOldRolls(id, rerollers);
        }

        public bool ShouldReRoll(List<Roll> startingRolls)
        {
            int highest = startingRolls.Max(x => x.Value);
            return startingRolls.Count(x => x.Value == highest) > 1;
        }

        public Lobby StartingRoll(Lobby lobby)
        {
            int playerCount = lobby.Players.Count();

            int rollCount = lobby.Rolls.Count();

            if (rollCount >= playerCount)
            {
                throw new Exception("All players have already rolled");
            }

            var player = lobby.Players[rollCount];
            var value = _diceService.RollDice();

            var newRoll = new Roll(player, value);
            lobby.Rolls.Add(newRoll);
            
            //todo: add this to test
            _lobbyService.UpdateLobby(lobby);

            return lobby;
        }
    }
}
