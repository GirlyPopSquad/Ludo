using LudoAPI.Models;

namespace LudoAPI.Services;

public interface IPieceService
{
    void SavePieces(int gameId, List<Piece> pieces);
    List<Piece> GetPieces(int gameId);
}