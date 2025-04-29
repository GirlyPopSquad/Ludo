using LudoAPI.Models;
using LudoAPI.Repositories;

namespace LudoAPI.Services
{
    public class GameService : IGameService
    {

        private IGameRepository _repository;
        private ILobbyRepository _lobbyRepository;

        public GameService(IGameRepository repository, ILobbyRepository lobbyRepository)
        {
            _repository = repository;
            _lobbyRepository = lobbyRepository;
        }

        public int CreateFromLobby(int lobbyId)
        {
            var lobby = _lobbyRepository.Get(lobbyId);
            
            var gamePlayers = lobby.Players.Select(lp => new GamePlayer(lp.Id)).ToList();
            var startingPlayer = lobby.Rolls.MaxBy(roll => roll.Value).Player.Id;

            var newGameId = _repository.Add(new Game(gamePlayers, startingPlayer));
            
            return newGameId;
        }

        public LobbyPlayer HaveTurn(Game game, LobbyPlayer player)
        {
            throw new NotImplementedException();
        }

        public void NewTurn()
        {
            //tager currentplayer til køen og sætter den næste player til currentplayer

            throw new NotImplementedException();
        }
    }
}