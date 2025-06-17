using LudoAPI.Models;

namespace LudoAPI.Repositories;

public interface IMovablePieceRepository
{
    void SetMovablePieces(int gameId, List<MovablePiece> movablePieces);
    MovablePiece? GetMovablePiece(int gameId, int pieceNumber);
}