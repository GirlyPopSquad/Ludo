using LudoAPI.Models;
using LudoAPI.Models.Tiles;

namespace LudoAPI.Services;

public class RuleService : IRuleService
{
    private readonly IRollService _rollService;
    private readonly IBoardService _boardService;
    private readonly IPieceService _pieceService;
    private readonly IGameService _gameService;
    

    public RuleService(IRollService rollService, IPieceService pieceService, IBoardService boardService, IGameService gameService)
    {
        _rollService = rollService;
        _pieceService = pieceService;
        _boardService = boardService;
        _gameService = gameService;
    }

    public bool DoesRollAllowLeavingHome(Roll roll)
    {
        return roll.Value == 6;
    }

    public bool PlayerIsAllowedAnotherRoll(int gameId)
    {
        var roll = _rollService.GetLastestRoll(gameId);

        var hasPlayerFinished = _gameService.HasPlayerFinished(gameId, roll.PlayerId);
        if (hasPlayerFinished)
        {
            return false;
        }
        
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

    public bool CanPiecePassCoordinate(int gameId, Piece piece, Coordinate coordinate)
    {
        var tile = _boardService.GetTileFromCoordinate(gameId, coordinate);

        //if the tile is an EndTile, this piece cant pass
        if (tile is EndTile) return false;

        var piecesOnCoordinate = _pieceService.GetPiecesFromCoordinate(gameId, coordinate);

        switch (piecesOnCoordinate.Length)
        {
            //if no other pieces on coordinate, this piece can pass
            case 0:
                return true;
            //if two or more other pieces are on the coordinate, this piece cant pass
            case >= 2:
                return false;
        }

        var pieceOfSameColor = piecesOnCoordinate.FirstOrDefault(p => p.Color == piece.Color);
        //if a piece of the same color is on the coordinate, then this piece cant pass.
        return pieceOfSameColor == null;
    }

    public bool WillThisPieceBeKickedHome(int gameId, Coordinate potentialCoordinate)
    {
        var piecesAtCoordinate = _pieceService.GetPiecesFromCoordinate(gameId, potentialCoordinate);

        return piecesAtCoordinate.Length == 2;
    }

    public Piece[] GetPiecesThatWillBeKickedHome(int gameId, MovablePiece chosenPiece)
    {
        var piecesAtCoordinate = _pieceService.GetPiecesFromCoordinate(gameId, chosenPiece.PotentialCoordinate);

        if (piecesAtCoordinate.Length is 2 or 0)
        {
            return [];
        }

        var piece = _pieceService.GetPiece(gameId, chosenPiece.PieceNumber);

        return piecesAtCoordinate.Where(p => p.Color != piece.Color).ToArray();
    }

    public bool WillThisBePlayersLastRound(int gameId, MovablePiece chosenPiece)
    {
        var nextTile = _boardService.GetTileFromCoordinate(gameId, chosenPiece.PotentialCoordinate);

        if (nextTile is not EndTile)
        {
            return false;
        }
        
        var piece = _pieceService.GetPiece(gameId, chosenPiece.PieceNumber);

        var playersEndTileCoordinates = _boardService.GetEndTilesFromColor(gameId, piece.Color).Select(t=> t.Coordinate).ToArray();
        var playersOtherPieces = _pieceService.GetPieces(gameId, (int)piece.Color)
            .Where(p => p.PieceNumber != piece.PieceNumber).ToArray();
        
        var playersPiecesAtEnd = playersOtherPieces.IntersectBy(playersEndTileCoordinates, p => p.Coordinate);

        //check if this is the last piece to make it to the end
        if (playersOtherPieces.Length == playersPiecesAtEnd.Count())
        {
            return true;
        }
        
        return false;
    }
}