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

    //TODO: for testing purposes, to easily be able to see result of board in UI.
    [HttpGet]
    public ActionResult<Board> Test()
    {
        var boardId = _boardService.InitStandardBoard(-1);
        var board = _boardService.GetBoard(boardId);

        return Ok(board);
    }

    [HttpGet("getByGameId/{gameId}")]
    public ActionResult<Board> Get(int gameId)
    {
        return Ok(_boardService.GetBoardFromGameId(gameId));
    }
}