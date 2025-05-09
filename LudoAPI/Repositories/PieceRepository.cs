using LudoAPI.Models;

namespace LudoAPI.Repositories;

public class PieceRepository : IPieceRepository
{
    //gameID and corresponding Pieces
    Dictionary<int, List<Piece>> _pieces = new();

    public void SavePieces(int gameId, List<Piece> pieces)
    {
        // todo check that gameId is valid
        
        _pieces.Add(gameId, pieces);   
    }

    public List<Piece> GetPiecesFromGameId(int gameId)
    {
        return _pieces[gameId];
    }
}