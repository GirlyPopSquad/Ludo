using LudoAPI.Models;

namespace LudoAPI.Repositories;

public interface IMovablePieceRepository
{
    void SetMovablePieces(int gameId, List<Piece> movablePieces);
    List<Piece> GetMovablePieces(int gameId);
}