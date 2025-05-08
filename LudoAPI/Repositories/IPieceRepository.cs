using LudoAPI.Models;

namespace LudoAPI.Repositories;

public interface IPieceRepository
{
    void SavePieces(int gameId, List<Piece> pieces);
    List<Piece> GetPiecesFromGameId(int gameId);
}