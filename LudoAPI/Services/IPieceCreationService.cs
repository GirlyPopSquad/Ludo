using LudoAPI.Models;
using LudoAPI.Models.Tiles;

namespace LudoAPI.Services;

public interface IPieceCreationService
{
    List<Piece> CreatePlayerPieces(HomeTile[] homeTiles);
}