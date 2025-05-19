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

    public Piece? GetPiece(int gameId, int pieceNumber)
    {
        return _pieceRepository.GetPiece(gameId, pieceNumber);
    }

    public Piece[] GetPiecesFromCoordinate(int gameId, Coordinate nextCoordinate)
    {
        return _pieceRepository.GetPiecesFromGameId(gameId)
            .Where(piece => piece.Coordinate == nextCoordinate).ToArray();
    }
}