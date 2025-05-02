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

        public int NextTurn(int gameId)
        {
            var game = _repository.Get(gameId);
            if(game.CurrentPlayerId == game.Players.Count)
            {
                game.CurrentPlayerId = 1;
            }
            else
            {
                game.CurrentPlayerId++;
            }
            _repository.Update(gameId, game);
            return game.CurrentPlayerId;
        }
    }
}