using LudoAPI.Models;
using LudoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LudoAPI.Controllers;

//TODO: for testing purposes, to easily be able to see result of board in UI.

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