using LudoAPI.Models;
using LudoAPI.Models.Tiles;

namespace LudoAPI.Services;

public class PieceCreationService : IPieceCreationService
{
    public List<Piece> CreatePlayerPieces(HomeTile[] homeTiles)
    {
        var pieceNumber = 1;
        var pieces = homeTiles
            .Select(tile =>
            {
                if (tile.Color == null)
                {
                    throw new Exception(
                        $"Error in Creating a piece: HomeTile doesnt have a Color, HomeTileCoordinate:{tile.Coordinate}");
                }

                var piece = new Piece(pieceNumber, (Color)tile.Color, tile.Coordinate);
                pieceNumber++;

                return piece;
            }).ToList();

        return pieces;
    }
}