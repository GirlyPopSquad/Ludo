using LudoAPI.Models;

namespace LudoAPI.Services;

public class MovablePieceService : IMovablePieceService
{
    private readonly IPieceService _pieceService;
    private readonly IGameService _gameService;
    private readonly IBoardService _boardService;
    private readonly IRollService _rollService;
    private readonly IRuleService _ruleService;

    public MovablePieceService(IPieceService pieceService, IGameService gameService, IBoardService boardService,
        IRollService rollService, IRuleService ruleService)
    {
        _pieceService = pieceService;
        _gameService = gameService;
        _boardService = boardService;
        _rollService = rollService;
        _ruleService = ruleService;
    }

    public List<Piece> GetMovablePieces(int gameId)
    {
        var latestRoll = _rollService.GetLastestRoll(gameId);
        var currentPlayerId = _gameService.GetCurrentPlayerId(gameId);

        if (latestRoll.PlayerId != currentPlayerId)
            // the person who made the last roll, is not the person, which turn it is. Therefore currentplayer needs to roll
        {
            throw new Exception("A new Roll has to be made, before movable pieces should be found");
        }

        var playerPieces = _pieceService.GetPieces(gameId, currentPlayerId);

        //determine whether all pieces are at home => then you get max three rolls

        var homeCoordinates = _boardService
            .GetHomeTiles(gameId)
            .Where(tile => tile.Color == (Color)currentPlayerId)
            .Select(tile => tile.Coordinate)
            .ToList();

        var piecesAtHome = playerPieces.IntersectBy(homeCoordinates, piece => piece.Coordinate).ToList();

        var movablePieces = new List<Piece>();

        if (piecesAtHome.Count != 0)
        {
            //todo: check if someone is blocking the starttile
            var piecesCanLeaveHome = _ruleService.DoesRollAllowLeavingHome(latestRoll);

            if (piecesCanLeaveHome)
            {
                movablePieces.AddRange(piecesAtHome);
            }
        }


        var canHaveAnotherTurn = _ruleService.PlayerIsAllowedAnotherRoll(gameId, latestRoll);

        if (movablePieces.Count == 0)
        {
            if (!canHaveAnotherTurn)
            {
                _gameService.NextTurn(gameId);
            }

            _gameService.UpdateIsTimeToRoll(gameId, true);
        }


        return movablePieces;
    }
}