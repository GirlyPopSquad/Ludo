using LudoAPI.Models;

namespace LudoAPI.Services;

public class GameStartingService : IGameStartingService
{
    private readonly IStartingRuleService _startingRuleService;
    private readonly IGameService _gameService;
    private readonly IBoardService _boardService;
    private readonly ILobbyService _lobbyService;
    private readonly IPieceCreationService _pieceCreationService;
    private readonly IPieceService _pieceService;

    public GameStartingService(IStartingRuleService startingRuleService, IGameService gameService,
        IBoardService boardService, IPieceService pieceService, ILobbyService lobbyService,
        IPieceCreationService pieceCreationService)
    {
        _startingRuleService = startingRuleService;
        _gameService = gameService;
        _boardService = boardService;
        _pieceService = pieceService;
        _lobbyService = lobbyService;
        _pieceCreationService = pieceCreationService;
    }

    public int SetupGame(int lobbyId)
    {
        //Find Lobby
        var lobby = _lobbyService.GetLobbyById(lobbyId);

        //Create Game
        var newGameId = CreateGame(lobby);

        //Delete Lobby
        _lobbyService.Delete(lobbyId);

        //Create board
        var boardId = _boardService.CreateStandardBoard(newGameId);
        var homeTiles = _boardService.GetHomeTiles(boardId);

        //Create Piece
        var pieces = _pieceCreationService.CreatePlayerPieces(homeTiles);
        _pieceService.SavePieces(newGameId, pieces);

        return newGameId;
    }

    private int CreateGame(Lobby lobby)
    {
        var startingPlayer = _startingRuleService.FindStartingPlayer(lobby);
        var game = new Game(lobby.Players, startingPlayer);
        
        var newGameId = _gameService.CreateGame(game);

        return newGameId;
    }
}