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
        private readonly IPieceService _pieceService;

        public GameService(IGameRepository gameRepository, ILobbyRepository lobbyRepository, IBoardService boardService, IPieceService pieceService)
        {
            _gameRepository = gameRepository;
            _lobbyRepository = lobbyRepository;
            _boardService = boardService;
            _pieceService = pieceService;
        }

        public int CreateFromLobby(int lobbyId)
        {
            var lobby = _lobbyRepository.Get(lobbyId);

            var gamePlayers = lobby.Players.Select(lp => new Player(lp.Id)).ToList();
            var startingPlayer = lobby.Rolls.MaxBy(roll => roll.Value).PlayerId;

            var newGameId = _gameRepository.Add(new Game(gamePlayers, startingPlayer));

            //add board
            var boardId = _boardService.InitStandardBoard(newGameId);
            var board = _boardService.GetBoard(boardId);


            var pieceNumber = 0;
            //create piece 1 pr. Hometile
            var homeTiles = board.Tiles.Values.OfType<HomeTile>().ToList();
            var pieces = homeTiles.Select(tile =>
            {
                var color = tile.Color;
                
                if (color != null)
                {
                    pieceNumber++;
                    return new Piece(pieceNumber, (Color) color, tile.Coordinate);
                }

                throw new Exception(
                    $"Error in Creating a piece: HomeTile doesnt have a Color, HomeTileCoordinate:{tile.Coordinate}");
                
            }).ToList();
            
             _pieceService.SavePieces(newGameId, pieces);
            
            return newGameId;
        }

        public int GetCurrentPlayerId(int gameId)
        {
            var game = _gameRepository.Get(gameId);
            return game.CurrentPlayerId;
        }

        public void NextTurn(int gameId)
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
        }

        public bool GetIsTimeToRoll(int gameId)
        {
            var game = _gameRepository.Get(gameId);
            return game.TimeToRoll;
        }

        public void UpdateIsTimeToRoll(int gameId, bool isTimeToRoll)
        {
            var game = _gameRepository.Get(gameId);
            game.TimeToRoll = isTimeToRoll;
            _gameRepository.Update(gameId, game);
        }
    }
}