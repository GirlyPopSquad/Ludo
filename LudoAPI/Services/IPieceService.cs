using LudoAPI.Models;

namespace LudoAPI.Services;

public interface IPieceService
{
    void SavePieces(int gameId, List<Piece> pieces);
    List<Piece> GetPieces(int gameId);
    List<Piece> GetPieces(int gameId, int playerId);
    void UpdatePiece(int gameId, Piece piece);
    Piece? GetPiece(int gameId, int pieceNumber);
    Piece[] GetPiecesFromCoordinate(int gameId, Coordinate nextCoordinate);
}