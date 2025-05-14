using LudoAPI.Models;

namespace LudoAPI.Repositories;

public interface IMovablePieceRepository
{
    void SetMovablePieces(int gameId, List<MovablePiece> movablePieces);
    List<MovablePiece> GetMovablePieces(int gameId);
    MovablePiece? GetPiece(int gameId, int pieceNumber);
}