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

    [HttpPost("create")]
    public ActionResult<int> CreateGame([FromBody] int lobbyId)
    {
        var game = _gameService.CreateFromLobby(lobbyId);
        return Ok(game);
    }
}