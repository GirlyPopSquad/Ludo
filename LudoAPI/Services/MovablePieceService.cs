using LudoAPI.Models;
using LudoAPI.Repositories;

namespace LudoAPI.Services;

public class MovablePieceService : IMovablePieceService
{
    private readonly IPieceService _pieceService;
    private readonly IGameService _gameService;
    private readonly IBoardService _boardService;
    private readonly IRollService _rollService;
    private readonly IRuleService _ruleService;
    private readonly IMovablePieceRepository _movablePieceRepository;

    public MovablePieceService(IPieceService pieceService, IGameService gameService, IBoardService boardService,
        IRollService rollService, IRuleService ruleService, IMovablePieceRepository movablePieceRepository)
    {
        _pieceService = pieceService;
        _gameService = gameService;
        _boardService = boardService;
        _rollService = rollService;
        _ruleService = ruleService;
        _movablePieceRepository = movablePieceRepository;
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

        var homeTiles = _boardService
            .GetHomeTiles(gameId)
            .Where(tile => tile.Color == (Color)currentPlayerId)
            .ToList();

        var homeCoordinates = homeTiles.Select(ht => ht.Coordinate);

        var piecesAtHome = playerPieces.IntersectBy(homeCoordinates, piece => piece.Coordinate).ToList();

        var movablePieces = new List<MovablePiece>();

        if (piecesAtHome.Count != 0)
        {
            //todo: check if someone is blocking the starttile
            var piecesCanLeaveHome = _ruleService.DoesRollAllowLeavingHome(latestRoll);

            if (piecesCanLeaveHome)
            {
                var movableHomePieces = piecesAtHome.Select(hp =>
                {
                    var homeTile = homeTiles.First(th => th.Coordinate == hp.Coordinate);
                    var nextCoordinate = homeTile.NextCoordinate(hp);
                    return new MovablePiece(hp.PieceNumber, nextCoordinate);
                });

                movablePieces.AddRange(movableHomePieces);
            }
        }

        //remove pieces at home - they are already handled
        var piecesLeft = playerPieces.Except(piecesAtHome).ToList();

        //remove pieces at end - they are finished
        var endTiles = _boardService.GetEndTiles(gameId);
        piecesLeft = piecesLeft
            .ExceptBy(endTiles
                .Select(t => t.Coordinate), piece => piece.Coordinate)
            .ToList();

        foreach (var piece in piecesLeft)
        {
            var currentTile = _boardService.GetTileFromCoordinate(gameId, piece.Coordinate);
            var rollValue = latestRoll.Value;

            var nextCoordinate = currentTile.NextCoordinate(piece);

            if (rollValue > 1)
            {
                for (var i = 1; i < rollValue; i++)
                {
                    var tempTile = _boardService.GetTileFromCoordinate(gameId, nextCoordinate);
                    nextCoordinate = tempTile.NextCoordinate(piece);

                    Console.WriteLine(nextCoordinate);
                }
            }

            //todo check if someone is blocking next coordinate    
            movablePieces.Add(new MovablePiece(piece.PieceNumber, nextCoordinate));
        }

        //todo: check if not-at-home-pieces actually are movable
        var canHaveAnotherTurn = _ruleService.PlayerIsAllowedAnotherRoll(gameId);

        if (movablePieces.Count == 0)
        {
            if (!canHaveAnotherTurn)
            {
                _gameService.NextTurn(gameId);
            }

            _gameService.UpdateIsTimeToRoll(gameId, true);
        }

        _movablePieceRepository.SetMovablePieces(gameId, movablePieces);

        var pieces = playerPieces
            .IntersectBy(movablePieces.Select(pt => pt.PieceNumber), piece => piece.PieceNumber)
            .ToList();

        return pieces;
    }

    public Piece MovePiece(int gameId, int pieceNumber)
    {
        var chosenPiece = _movablePieceRepository.GetPiece(gameId, pieceNumber);
        if (chosenPiece == null)
        {
            throw new Exception("Piece is not movable at this point");
        }

        var piece = _pieceService.GetPiece(gameId, pieceNumber);

        piece!.Coordinate = chosenPiece.PotentialCoordinate;
        _pieceService.UpdatePiece(gameId, piece);

        var canRoleAgain = _ruleService.PlayerIsAllowedAnotherRoll(gameId);
        if (!canRoleAgain)
        {
            _gameService.NextTurn(gameId);
        }

        _gameService.UpdateIsTimeToRoll(gameId, true);

        return piece;
    }
}