using LudoAPI.Models;

namespace LudoAPI.Repositories;

public interface IMovablePieceRepository
{
    void SetMovablePieces(int gameId, List<MovablePiece> movablePieces);
    MovablePiece? GetPiece(int gameId, int pieceNumber);
}