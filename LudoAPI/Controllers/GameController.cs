using LudoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LudoAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GameController: ControllerBase
{
    private readonly IGameService _gameService;
    private readonly IGameStartingService _gameStartingService;

    public GameController(IGameService gameService, IGameStartingService gameStartingService)
    {
        _gameService = gameService;
        _gameStartingService = gameStartingService;
    }
    
    [HttpPost("create/{lobbyId}")]
    public ActionResult<int> CreateGame(int lobbyId)
    {
        var gameId = _gameStartingService.SetupGame(lobbyId);
        
        return Ok(gameId);
    }

    [HttpPost("getCurrentPlayerId/{gameId}")]
    public ActionResult<int> GetCurrentPlayerId(int gameId)
    {
        var currentPlayerId = _gameService.GetCurrentPlayerId(gameId);
        return Ok(currentPlayerId);
    }
}