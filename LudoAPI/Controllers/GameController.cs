using LudoAPI.Models;
using LudoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LudoAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GameController
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    public ActionResult<Game> StartGame([FromBody] Lobby lobby)
    {
        throw new NotImplementedException();
    }
    
    
}