using LudoAPI.Models;

namespace LudoAPI.Repositories;

public class PieceRepository : IPieceRepository
{
    //gameID and corresponding Pieces
    Dictionary<int, List<Piece>> _pieces = new();

    public void SavePieces(int gameId, List<Piece> pieces)
    {
        _pieces.Add(gameId, pieces);
    }

    public List<Piece> GetPiecesFromGameId(int gameId)
    {
        return _pieces[gameId];
    }

    public void UpdatePiece(int gameId, Piece piece)
    {
        var pieces = _pieces[gameId];
        var existingPiece = pieces.FirstOrDefault(p => p.PieceNumber == piece.PieceNumber);
        if (existingPiece != null)
        {
            existingPiece.Coordinate = piece.Coordinate;
        }
    }

    public Piece GetPiece(int gameId, int pieceNumber)
    {
        return _pieces[gameId]
            .Find(p => p.PieceNumber == pieceNumber);
    }
}