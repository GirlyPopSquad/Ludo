namespace LudoAPI.Models;

public class Board
{
    public int Id { get; }
    public int GameId { get; }
    public int Rows { get; }
    public int Cols { get; }
    public Dictionary<string, Tile> Tiles { get; }

    public Board(int id, int gameId, Dictionary<string, Tile> tiles, int rows, int cols)
    {
        Id = id;
        GameId = gameId;
        Tiles = tiles;
        Rows = rows;
        Cols = cols;
    }
}