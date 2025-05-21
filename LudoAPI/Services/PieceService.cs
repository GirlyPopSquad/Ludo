using LudoAPI.Models;
using LudoAPI.Repositories;

namespace LudoAPI.Services;

public class PieceService : IPieceService
{
    private readonly IPieceRepository _pieceRepository;

    public PieceService(IPieceRepository pieceRepository)
    {
        _pieceRepository = pieceRepository;
    }

    public void SavePieces(int gameId, List<Piece> pieces)
    {
        _pieceRepository.SavePieces(gameId, pieces);
    }

    public List<Piece> GetPieces(int gameId)
    {
        return _pieceRepository.GetPiecesFromGameId(gameId);
    }

    public List<Piece> GetPieces(int gameId, int playerId)
    {
        return _pieceRepository.GetPiecesFromGameId(gameId)
            .Where(piece => piece.Color == (Color)playerId).ToList();
    }

    public void UpdatePiece(int gameId, Piece piece)
    {
        _pieceRepository.UpdatePiece(gameId, piece);
    }

    public Piece GetPiece(int gameId, int pieceNumber)
    {
        var piece = _pieceRepository.GetPiece(gameId, pieceNumber);

        if (piece == null)
        {
            throw new Exception($"No piece exists with this pieceNumber: {pieceNumber} in this game with id: {gameId}");
        }

        return piece;
    }

    public Piece[] GetPiecesFromCoordinate(int gameId, Coordinate nextCoordinate)
    {
        return _pieceRepository.GetPiecesFromGameId(gameId)
            .Where(piece => piece.Coordinate == nextCoordinate).ToArray();
    }

    public Piece[] GetPiecesFromColor(int gameId, Color pieceColor)
    {
        return _pieceRepository.GetPiecesFromGameId(gameId)
            .Where(piece => piece.Color == pieceColor).ToArray();
    }
}