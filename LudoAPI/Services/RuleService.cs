using LudoAPI.Models;

namespace LudoAPI.Services;

public class RuleService : IRuleService
{
    private readonly IRollService _rollService;
    private readonly IBoardService _boardService;
    private readonly IPieceService _pieceService;

    public RuleService(IRollService rollService, IPieceService pieceService, IBoardService boardService)
    {
        _rollService = rollService;
        _pieceService = pieceService;
        _boardService = boardService;
    }

    public bool DoesRollAllowLeavingHome(Roll roll)
    {
        return roll.Value == 6;
    }

    public bool PlayerIsAllowedAnotherRoll(int gameId)
    {
        
        var roll = _rollService.GetLastestRoll(gameId);
        //if player rolled a 6, they can roll again
        if (roll.Value == 6) return true;
        
        var last3Rolls = _rollService.GetLatestRolls(gameId, 3);

        //is player has all pieces at home, and has rolled less than 3 times in a row, they can roll again
        var playerPieces = _pieceService.GetPieces(gameId, roll.PlayerId);
        var homeCoords = _boardService.GetHomeTiles(gameId).Where(t => t.Color == (Color?)roll.PlayerId)
            .Select(tile => tile.Coordinate).ToList();

        var piecesAtHome = playerPieces.IntersectBy(homeCoords, piece => piece.Coordinate).ToList();

        var isAllPiecesHome = piecesAtHome.Count == homeCoords.Count;
        if (!isAllPiecesHome) return false;

        var hasRolledLessThan3TimesInARow = last3Rolls.Count(r => r.PlayerId == roll.PlayerId) < 3;
        return hasRolledLessThan3TimesInARow;
    }

    public bool CanPiecePassTroughCoordinate(int gameId, Piece piece, Coordinate nextCoordinate)
    {
        var piecesOnCoordinate = _pieceService.GetPiecesFromCoordinate(gameId, nextCoordinate);

        switch (piecesOnCoordinate.Length)
        {
            case 0:
                return true;
            case >= 2:
                return false;
        }

        var pieceOfSameColor = piecesOnCoordinate.FirstOrDefault(p=> p.Color == piece.Color);
        return pieceOfSameColor == null;
    }
}