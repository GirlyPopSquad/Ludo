using LudoAPI.Models;
using LudoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LudoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PieceController
{
    private readonly IPieceService _pieceService;

    public PieceController(IPieceService pieceService)
    {
        _pieceService = pieceService;
    }

    [HttpGet("GetByGameId/{gameId}")]
    public List<Piece> GetPiecesFromGameId(int gameId)
    {
        return _pieceService.GetPieces(gameId);
    }
    
}