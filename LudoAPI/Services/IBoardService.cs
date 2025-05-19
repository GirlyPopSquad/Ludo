using LudoAPI.Models;
using LudoAPI.Models.Tiles;

namespace LudoAPI.Services;

public interface IBoardService
{
    int InitStandardBoard(int gameId);
    Board GetBoard(int boardId);
    Board GetBoardFromGameId(int gameId);
    List<HomeTile> GetHomeTiles(int gameId);
    Tile GetTileFromCoordinate(int gameId, Coordinate coordinate);
    List<EndTile> GetEndTiles(int gameId);
    HomeTile[] GetHomeTilesFromColor(int gameId, Color pieceColor);
}