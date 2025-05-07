using LudoAPI.Models;
using LudoAPI.Models.Tiles;
using LudoAPI.Repositories;

namespace LudoAPI.Services
{
    public class GameService : IGameService
    {

        private readonly IGameRepository _gameRepository;
        private readonly ILobbyRepository _lobbyRepository;
        private readonly IBoardService _boardService;

        public GameService(IGameRepository gameRepository, ILobbyRepository lobbyRepository)
        {
            _gameRepository = gameRepository;
            _lobbyRepository = lobbyRepository;
        }

        public int CreateFromLobby(int lobbyId)
        {
            var lobby = _lobbyRepository.Get(lobbyId);
            
            var gamePlayers = lobby.Players.Select(lp => new GamePlayer(lp.Id)).ToList();
            var startingPlayer = lobby.Rolls.MaxBy(roll => roll.Value).Player.Id;

            var newGameId = _gameRepository.Add(new Game(gamePlayers, startingPlayer));
            
            //add board
            var board = _boardService.InitStandardBoard(newGameId);
            
            //create piece 1 pr. Hometile
            var homeTiles = board.Tiles.Values.OfType<HomeTile>().ToList();
            
            
            return newGameId;
        }

        public int GetCurrentPlayerId(int gameId)
        {
            var game = _gameRepository.Get(gameId);
            return game.CurrentPlayerId;
        }

        public int NextTurn(int gameId)
        {
            var game = _gameRepository.Get(gameId);
            if(game.CurrentPlayerId == game.Players.Count)
            {
                game.CurrentPlayerId = 1;
            }
            else
            {
                game.CurrentPlayerId++;
            }
            _gameRepository.Update(gameId, game);
            return game.CurrentPlayerId;
        }
    }
}