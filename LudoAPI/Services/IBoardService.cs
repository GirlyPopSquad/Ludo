using LudoAPI.Models;
using LudoAPI.Models.Tiles;

namespace LudoAPI.Services;

public interface IBoardService
{
    int CreateStandardBoard(int gameId);
    Board GetBoardFromGameId(int gameId);
    HomeTile[] GetHomeTiles(int gameId);
    Tile GetTileFromCoordinate(int gameId, Coordinate coordinate);
    List<EndTile> GetEndTiles(int gameId);
    HomeTile[] GetHomeTilesFromColor(int gameId, Color pieceColor);
    EndTile[] GetEndTilesFromColor(int gameId, Color pieceColor);
}