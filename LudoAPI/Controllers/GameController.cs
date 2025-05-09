using LudoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LudoAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GameController: ControllerBase
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpPost("create/{lobbyId}")]
    public ActionResult<int> CreateGame(int lobbyId)
    {
        var gameId = _gameService.CreateFromLobby(lobbyId);
        return Ok(gameId);
    }

    [HttpPost("getCurrentPlayerId/{gameId}")]
    public ActionResult<int> GetCurrentPlayerId(int gameId)
    {
        var currentPlayerId = _gameService.GetCurrentPlayerId(gameId);
        return Ok(currentPlayerId);
    }

    [HttpPost("nextturn/{gameId}")]
    public ActionResult<int> NextTurn(int gameId)
    {
        var currentPlayerId = _gameService.NextTurn(gameId);
        return Ok(currentPlayerId);
    }
}