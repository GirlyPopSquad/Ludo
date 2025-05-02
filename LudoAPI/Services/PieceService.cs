using LudoAPI.Models;
using LudoAPI.Repositories;

namespace LudoAPI.Services
{
    public class PieceService
    {
        private IGameRepository _repository;

        public PieceService(IGameRepository repository)
        {
            _repository = repository;
        }

        public Piece[] CreatePieces(int playerId, int gameId)
        {
            // create pieces for a player
            Piece[] pieces = new Piece[4];
            for (int i = 0; i < 4; i++)
            {
                pieces[i] = new Piece(playerId);
            }
            return pieces;
        }

        public List<Piece> GetMovablePieces(int gameId, int playerId)
        {
            throw new NotImplementedException();
            // check all movable pieces for this player
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
