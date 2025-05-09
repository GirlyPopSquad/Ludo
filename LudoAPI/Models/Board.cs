using LudoAPI.Models.Tiles;

namespace LudoAPI.Models;

public class Board
{
    public int Id { get; }
    public int GameId { get; }
    public int Rows { get; }
    public int Cols { get; }
    public Dictionary<string, Tile> Tiles { get; }

    public Board(int gameId, Dictionary<string, Tile> tiles, int rows, int cols)
    {
        Id = -1;
        GameId = gameId;
        Tiles = tiles;
        Rows = rows;
        Cols = cols;
    }

    public Board(int id, Board board)
    {
        Id = id;
        GameId = board.GameId;
        Tiles = board.Tiles;
        Rows = board.Rows;
        Cols = board.Cols;
    }
}