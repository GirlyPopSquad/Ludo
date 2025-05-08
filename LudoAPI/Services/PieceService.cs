using LudoAPI.Models;
using LudoAPI.Repositories;

namespace LudoAPI.Services;

public class PieceService : IPieceService
{
    private readonly IPieceRepository _repository;

    public PieceService(IPieceRepository repository)
    {
        _repository = repository;
    }

    public void SavePieces(int gameId, List<Piece> pieces)
    {
        _repository.SavePieces(gameId, pieces);
    }

    public List<Piece> GetPieces(int gameId)
    {
        return _repository.GetPiecesFromGameId(gameId);
    }
}