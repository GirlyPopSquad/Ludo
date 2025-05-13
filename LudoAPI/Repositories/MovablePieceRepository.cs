using LudoAPI.Models;

namespace LudoAPI.Repositories;

public class MovablePieceRepository : IMovablePieceRepository
{
    private readonly Dictionary<int, List<Piece>> _movablePieces = new();

    public void SetMovablePieces(int gameId, List<Piece> movablePieces)
    {
        _movablePieces[gameId] = movablePieces;
    }

    public List<Piece> GetMovablePieces(int gameId)
    {
        return _movablePieces[gameId];
    }
}