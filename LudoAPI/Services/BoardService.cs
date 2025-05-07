using LudoAPI.Models;
using LudoAPI.Models.Tiles;
using LudoAPI.Services.Utils;

namespace LudoAPI.Services;

public class BoardService : IBoardService
{
    public Board MakeBoardFromMap(int gameId, string[,] boardMap)
    {
        Dictionary<string, Tile> tiles = IdentifyTiles(boardMap);

        var startTiles = tiles.Values.OfType<StartTile>().ToList();
        var homeTiles = tiles.Values.OfType<HomeTile>().ToArray();

        //setup home tiles - happens here because StartTiles needs to be identified first
        SetupHomeTiles(homeTiles, startTiles);

        int rows = boardMap.GetLength(0);
        int cols = boardMap.GetLength(1);

        return new Board(gameId, tiles, rows, cols);
    }

    public Board InitStandardBoard(int i)
    {
        return MakeBoardFromMap(i, BoardMapLibrary.StandardBoard);
    }

    private Dictionary<string, Tile> IdentifyTiles(string[,] map)
    {
        Dictionary<string, Tile> tiles = new();
        //convert map to tiles
        for (int row = 0; row < map.GetLength(0); row++)
        {
            for (int col = 0; col < map.GetLength(1); col++)
            {
                var coordinate = new Coordinate(col, row);
                var tileString = map[row, col];

                var tile = TranslateToTile(tileString, coordinate);
                tiles.Add(coordinate.ToString(), tile);
            }
        }

        return tiles;
    }

    private HomeTile[] SetupHomeTiles(HomeTile[] homeTiles, List<StartTile> startTiles)
    {
        return homeTiles.Select(ht =>
        {
            ht.StartTiles = startTiles.Where(tile => tile.Color == ht.Color).ToArray();
            return ht;
        }).ToArray();
    }

    //todo: could be moved to different class
    private Tile TranslateToTile(string tileString, Coordinate coordinate)
    {
        switch (tileString)
        {
            case "":
                return new Tile(coordinate);
            case "L": //left
                return new Tile(coordinate, new Move(-1, 0));
            case "R": //right
                return new Tile(coordinate, new Move(+1, 0));
            case "U": //up
                return new Tile(coordinate, new Move(0, 1));
            case "D": //down
                return new Tile(coordinate, new Move(0, -1));
            case "UL": //Up-Left
                return new Tile(coordinate, new Move(-1, +1));
            case "UR": //Up-Right
                return new Tile(coordinate, new Move(1, +1));
            case "DL": //Down-Left
                return new Tile(coordinate, new Move(-1, -1));
            case "DR": //Down-Right:
                return new Tile(coordinate, new Move(1, -1));
            case "r": //red
                return new Tile(coordinate, Color.Red);
            case "rL": //redLeft
                return new Tile(coordinate, Color.Red, new Move(-1, 0));
            case "rH": //redHome
                return new HomeTile(coordinate, Color.Red);
            case "rE": //redEnd
                return new EndTile(coordinate, Color.Red);
            case "rSR": //red Start Right
                return new StartTile(coordinate, Color.Red, new Move(1, 0));
            case "rR": //redRight
                return new Tile(coordinate, Color.Red, new Move(1, 0));
            case "rR-U": //redRight else UP
                return new ArrowTile(coordinate, Color.Red, new Move(0, 1), new Move(1, 0));
            case "g": //green
                return new Tile(coordinate, Color.Green);
            case "gU": //greenUp
                return new Tile(coordinate, Color.Green, new Move(0, 1));
            case "gD": //greenDown
                return new Tile(coordinate, Color.Green, new Move(0, -1));
            case "gD-R": //greenDown else Right
                return new ArrowTile(coordinate, Color.Green, new Move(1, 0), new Move(0, -1));
            case "gH": // greenhome
                return new HomeTile(coordinate, Color.Green);
            case "gE":
                return new EndTile(coordinate, Color.Green);
            case "gSD":
                return new StartTile(coordinate, Color.Green, new Move(0, -1));
            case "b":
                return new Tile(coordinate, Color.Blue);
            case "bL":
                return new Tile(coordinate, Color.Blue, new Move(-1, 0));
            case "bH":
                return new HomeTile(coordinate, Color.Blue);
            case "bE":
                return new EndTile(coordinate, Color.Blue);
            case "bSU":
                return new StartTile(coordinate, Color.Blue, new Move(0, 1));
            case "bU":
                return new Tile(coordinate, Color.Blue, new Move(0, 1));
            case "bD":
                return new Tile(coordinate, Color.Blue, new Move(0, -1));
            case "bU-L":
                return new ArrowTile(coordinate, Color.Blue, new Move(-1, 0), new Move(0, 1));
            case "y":
                return new Tile(coordinate, Color.Yellow);
            case "yH":
                return new HomeTile(coordinate, Color.Yellow);
            case "yE":
                return new EndTile(coordinate, Color.Yellow);
            case "ySL":
                return new StartTile(coordinate, Color.Yellow, new Move(-1, 0));
            case "yL": // yellow Left
                return new Tile(coordinate, Color.Yellow, new Move(-1, 0));
            case "yL-D": //yellow Left else Down
                return new ArrowTile(coordinate, Color.Yellow, new Move(0, -1), new Move(-1, 0));
            default:
                return new Tile(coordinate);
        }
    }
}