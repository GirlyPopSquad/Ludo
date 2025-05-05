using LudoAPI.Models;
using LudoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LudoAPI.Controllers;

//TODO: for testing purposes, too se result of board in UI

[Route("api/[controller]")]
[ApiController]
public class BoardController : ControllerBase
{
    private readonly IBoardService _boardService;

    public BoardController(IBoardService boardService)
    {
        _boardService = boardService;
    }


    [HttpGet]
    public ActionResult<Board> Test()
    {
        var board = _boardService.InitStandardBoard(-1);

        return Ok(board);
    }
}