using LudoAPI.Models;

namespace LudoAPI.Services
{
    public class StartingService : IStartingService
    {
        private readonly IPlayerService _playerService;
        private readonly IDiceService _diceService;
        private readonly ILobbyService _lobbyService;

        public StartingService(IDiceService diceService, ILobbyService lobbyService)
        {
            _diceService = diceService;
            _lobbyService = lobbyService;
        }

        public Lobby HandleRerolls(Lobby lobby)
        {
            throw new NotImplementedException();
        }

        public Lobby HandleReroll(int lobbyId, Roll roll)
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

            int rollCount = lobby.Rolls.Count();

            if (rollCount >= playerCount)
            {
                throw new Exception("All players have already rolled");
            }

            var player = lobby.Players[rollCount];
            var value = _diceService.RollDice();

            var newRoll = new Roll(player, value);
            lobby.Rolls.Add(newRoll);

            return lobby;
        }
    }
}
