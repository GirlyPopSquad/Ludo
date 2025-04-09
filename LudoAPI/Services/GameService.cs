using LudoAPI.Models;
using LudoAPI.Repositories;

namespace LudoAPI.Services
{
    public class GameService : IGameService
    {
        private IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }


        public Game NewGame(Lobby lobby)
        {
            var rolls = lobby.Rolls;
            var highestRoll = rolls.OrderByDescending(r => r.Value).FirstOrDefault();

            var startPieces = new List<Piece>
            {
                new Piece(1),
                new Piece(2),
                new Piece(3),
                new Piece(4),
            };

            var gamePlayers = lobby.Players
                .Select(lp => new Player(lp.Id, startPieces))
                .ToList();


            var game = _gameRepository.NewGame(highestRoll.Player.Id, gamePlayers);
            return game;
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