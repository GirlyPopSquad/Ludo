using LudoAPI.Models;
using LudoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LudoAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BoardController : ControllerBase
{
    private readonly IBoardService _boardService;

    public BoardController(IBoardService boardService)
    {
        _boardService = boardService;
    }

    [HttpGet("getByGameId/{gameId}")]
    public ActionResult<Board> Get(int gameId)
    {
        return Ok(_boardService.GetBoardFromGameId(gameId));
    }
}