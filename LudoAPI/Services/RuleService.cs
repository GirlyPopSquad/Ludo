using LudoAPI.Models;

namespace LudoAPI.Services
{
    public class RuleService
    {
        private readonly IPieceService _pieceService;
        public RuleService(IPieceService pieceService)
        {
            _pieceService = pieceService;
        }

        public List<Piece> GetMovablePieces(int gameId, Color color)
        {
            var pieces = _pieceService.GetPieces(gameId);

            var playerPieces = pieces.Where(p => p.Color == color).ToList();



            throw new NotImplementedException();
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
