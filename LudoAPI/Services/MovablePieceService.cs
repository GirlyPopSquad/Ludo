using LudoAPI.Models;
using LudoAPI.Models.Tiles;
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
            var isPieceMovable = true;

            var nextCoordinate = currentTile.NextCoordinate(piece);

            if (rollValue > 1)
            {
                //handle intermediate coordinates
                for (var i = 1; i < rollValue; i++)
                {
                    var canPassTroughCoordinate =
                        _ruleService.CanPiecePassTroughCoordinate(gameId, piece, nextCoordinate);
                    if (!canPassTroughCoordinate)
                    {
                        isPieceMovable = false;
                        continue;
                    }

                    var tempTile = _boardService.GetTileFromCoordinate(gameId, nextCoordinate);
                    nextCoordinate = tempTile.NextCoordinate(piece);
                }
            }

            if (isPieceMovable)
            {
                movablePieces.Add(new MovablePiece(piece.PieceNumber, nextCoordinate));
            }
        }

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
        //todo: check if someone is blocking the tile
        var chosenPiece = _movablePieceRepository.GetPiece(gameId, pieceNumber);
        if (chosenPiece == null)
        {
            throw new Exception("Piece is not movable at this point");
        }

        var piece = _pieceService.GetPiece(gameId, pieceNumber);
        if (piece == null)
        {
            //todo move to method
            throw new Exception("No piece exists with this pieceNumber in this game");
        }

        var predictedCoordinate = chosenPiece.PotentialCoordinate;
        var finalCoordinate = predictedCoordinate;
        var willThisPieceBeKickedHome = _ruleService.WillThisPieceBeKickedHome(gameId, predictedCoordinate);
        if (willThisPieceBeKickedHome)
        {
            finalCoordinate = FindAvailableHomeCoordinate(gameId, piece);
        }

        var piecesToKick = _ruleService.GetPiecesThatWillBeKickedHome(gameId, chosenPiece);

        foreach (var pieceToBeKicked in piecesToKick)
        {
            KickPieceHome(gameId, pieceToBeKicked);
        }

        piece.Coordinate = finalCoordinate;
        _pieceService.UpdatePiece(gameId, piece);

        var canRoleAgain = _ruleService.PlayerIsAllowedAnotherRoll(gameId);
        if (!canRoleAgain)
        {
            _gameService.NextTurn(gameId);
        }

        _gameService.UpdateIsTimeToRoll(gameId, true);

        return piece;
    }

    private void KickPieceHome(int gameId, Piece piece)
    {
        var homeCoordinate = FindAvailableHomeCoordinate(gameId, piece);
        piece.Coordinate = homeCoordinate;
        _pieceService.UpdatePiece(gameId, piece);
    }


    //todo: could be moved to boardService?
    private Coordinate FindAvailableHomeCoordinate(int gameId, Piece piece)
    {
        var homeTiles = _boardService.GetHomeTilesFromColor(gameId, piece.Color);

        if (homeTiles.Any(t => t.Coordinate == piece.Coordinate))
        {
            //piece is already home and will stay there.
            return piece.Coordinate;
        }
        
        var coordinatesOfPlayersPieces = _pieceService.GetPiecesFromColor(gameId, piece.Color)
            .Select(p => p.Coordinate);

        var availableHomeTile = homeTiles.ExceptBy(coordinatesOfPlayersPieces, tile => tile.Coordinate)
            .First();
        return availableHomeTile.Coordinate;
    }
}