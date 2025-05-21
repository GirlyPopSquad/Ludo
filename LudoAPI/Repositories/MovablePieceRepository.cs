using LudoAPI.Models;

namespace LudoAPI.Repositories;

public class MovablePieceRepository : IMovablePieceRepository
{
    private readonly Dictionary<int, List<MovablePiece>> _movablePieces = new();

    public void SetMovablePieces(int gameId, List<MovablePiece> movablePieces)
    {
        _movablePieces[gameId] = movablePieces;
    }

    public List<MovablePiece> GetMovablePieces(int gameId)
    {
        return _movablePieces[gameId];
    }

    public MovablePiece? GetPiece(int gameId, int pieceNumber)
    {
        return _movablePieces[gameId].FirstOrDefault(piece=> piece.PieceNumber == pieceNumber);
    }
}