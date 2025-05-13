using LudoAPI.Models;

namespace LudoAPI.Services;

public interface IMovablePieceService
{
    public List<Piece> GetMovablePieces(int gameId);
    
    public Piece MovePiece(int gameId, int pieceNumber);
}