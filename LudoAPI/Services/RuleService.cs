using LudoAPI.Models;
using LudoAPI.Models.Tiles;

namespace LudoAPI.Services
{
    public class RuleService
    {
        private readonly IBoardService _boardService;
        private readonly IPieceService _pieceService;
        public RuleService(IPieceService pieceService, IBoardService boardService)
        {
            _pieceService = pieceService;
            _boardService = boardService;
        }

        public List<Piece> GetMovablePieces(int gameId, Color color, int roll)
        {
            var pieces = _pieceService.GetPieces(gameId);

            var playerPieces = pieces.Where(p => p.Color == color).ToList();

            var tiles = _boardService.GetBoardFromGameId(gameId).Tiles;

            foreach(var playerPiece in playerPieces)
            {
                var tile = tiles[playerPiece.Coordinate.ToString()];
                for (int i = 1; i <= roll; i++)
                {
                    var nextCoordinate = tile.NextCoordinate(playerPiece);

                    if(CheckIfMultipleEnemyPieces(nextCoordinate, pieces, playerPiece.Color))
                    {
                        if(i == roll)
                        {
                            var homeTiles = tiles.Values.OfType<HomeTile>().ToArray();
                            var myHomeTiles = homeTiles.Where(t => t.Color == playerPiece.Color).ToArray();
                        }
                    }
                   

                    tile = tiles[nextCoordinate.ToString()];
                }
            }

            throw new NotImplementedException();
        }

        public void KickHome(Dictionary<string, Tile> tiles, Piece playerPiece)
        {
            var homeTiles = tiles.Values.OfType<HomeTile>().ToArray();
            var myHomeTiles = homeTiles.Where(t => t.Color == playerPiece.Color).ToArray();
            playerPiece.Coordinate = myHomeTiles[0].Coordinate;
        }

        public bool CheckIfMultipleEnemyPieces(Coordinate coordinate, List<Piece> pieces, Color color)
        {
            int count = 0;
            foreach (var piece in pieces)
            {
                if (piece.Coordinate == coordinate)
                {
                    if (piece.Color == color)
                    {
                        count++;
                    }
                }
            }

            if(count > 1)
            {
                return true;
            }
            else
            {

               return false;
            }
        }
    

        public bool IsMovable(Piece piece)
        {


            throw new NotImplementedException();
            // check one piece if it is movable. probably needs to get a list of all pieces or players to check it.

            //rules
            // if pieces are at home they can only move with a 6
            // cant jump over your own pieces
        }

        public bool IsKickedHome(Piece piece)
        {
            throw new NotImplementedException();
            // check if a piece is kicked home

            // rules
            // if a piece lands on another piece of different color, the other piece is kicked home
            // if a piece lands on 2 other pieces of different color, it is kicked home
        }
    }
}
