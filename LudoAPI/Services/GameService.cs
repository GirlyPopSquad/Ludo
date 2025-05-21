using LudoAPI.Models;
using LudoAPI.Repositories;

namespace LudoAPI.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public int GetCurrentPlayerId(int gameId)
        {
            var game = _gameRepository.Get(gameId);
            return game.CurrentPlayerId;
        }

        public void NextTurn(int gameId)
        {
            if (HasGameEnded(gameId))
            {
                return;
            }
            
            var game = _gameRepository.Get(gameId);
            
            var playingPlayers = game.Players.ExceptBy(game.FinishedPlayerIds, player => player.Id ).ToArray();
            var playerWithHigherIds = playingPlayers.Where(player => player.Id > game.CurrentPlayerId).ToList();

            if(playerWithHigherIds.Count == 0)
            {
                game.CurrentPlayerId = playingPlayers.Min(player => player.Id);
            }
            else
            {
                game.CurrentPlayerId = playerWithHigherIds.First().Id;
            }
            
            _gameRepository.Update(gameId, game);
        }

        public bool GetIsTimeToRoll(int gameId)
        {
            var game = _gameRepository.Get(gameId);
            return game.TimeToRoll && !HasGameEnded(gameId);
        }

        public void UpdateIsTimeToRoll(int gameId, bool isTimeToRoll)
        {
            if (HasGameEnded(gameId))
            {
                return;
            }
            
            var game = _gameRepository.Get(gameId);
            game.TimeToRoll = isTimeToRoll;
            _gameRepository.Update(gameId, game);
        }

        public void HandlePlayerFinished(int gameId, Color pieceColor)
        {
           var game = _gameRepository.Get(gameId);
           game.FinishedPlayerIds.Add((int)pieceColor);
           _gameRepository.Update(gameId, game);
        }

        public bool HasPlayerFinished(int gameId, int playerId)
        {
            var game = _gameRepository.Get(gameId);
            return game.FinishedPlayerIds.Contains(playerId);
        }

        public bool HasGameEnded(int gameId)
        {
            var game = _gameRepository.Get(gameId);
            
            return game.FinishedPlayerIds.Count >= game.Players.Count-1;
        }

        public int CreateGame(Game game)
        {
            return _gameRepository.Add(game);
        }
    }
}