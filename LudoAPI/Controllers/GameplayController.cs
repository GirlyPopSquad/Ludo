using LudoAPI.Models;
using LudoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LudoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameplayController: ControllerBase
{
    private readonly IMovablePieceService _movablePieceService;

    public GameplayController(IMovablePieceService movablePieceService)
    {
        _movablePieceService = movablePieceService;
    }

    [HttpGet("movablePieces/{gameId}")]
    public ActionResult<List<Piece>> GetMovablePieces(int gameId)
    {
        return Ok(_movablePieceService.GetMovablePieces(gameId));
    }
}