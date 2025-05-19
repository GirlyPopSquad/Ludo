using LudoAPI.Models;

namespace LudoAPI.Repositories;

public interface IPieceRepository
{
    void SavePieces(int gameId, List<Piece> pieces);
    List<Piece> GetPiecesFromGameId(int gameId);
    void UpdatePiece(int gameId, Piece piece);
    Piece? GetPiece(int gameId, int pieceNumber);
}