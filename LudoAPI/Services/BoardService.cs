using LudoAPI.Models;

namespace LudoAPI.Services;

public class BoardService : IBoardService
{
    public Board InitStandardBoard(int gameId)
    {
        var schema = _standardBoard;
        int rows = schema.GetLength(0);
        int cols = schema.GetLength(1);
        Dictionary<string, Tile> tiles = new();

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                var coordinate = new Coordinate(col, row);
                var tileString = schema[row, col];

                var tile = TranslateToTile(tileString, coordinate);
                tiles.Add(coordinate.ToString(), tile);
            }
        }

        return new Board(-1, gameId,tiles, rows, cols);
    }

    //Todo handle "*" 
    //TODO handle "end"(winning) tiles
    //TODO handle moves for the inner corners - needs a name maybe like "UR" for Up-Right?
    //TODO handle starttile (the tile you land on after the first 6). the move from "home" to "start"
    
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
            case "r": //red
                return new Tile(coordinate,Color.Red);
            case "rL": //redLeft
                return new Tile(coordinate,Color.Red, new Move(-1,0));
            case "rH": //redhome
                return new Tile(coordinate, Color.Red);
            case "rR": //redRight
                return new Tile(coordinate, Color.Red, new Move(1,0));
            case "rR-U": //redRight else UP
                return new ArrowTile(coordinate,Color.Red, new Move(0, 1), new Move(1,0));
            case "g":  //green
                return new Tile(coordinate, Color.Green);
            case "gU": //greenUp
                return new Tile(coordinate, Color.Green, new Move(0, 1));
            case "gD": //greenDown
                return new Tile(coordinate, Color.Green, new Move(0,-1));
            case "gD-R": //greenDown else Right
                return new ArrowTile(coordinate, Color.Green, new Move(1, 0), new Move(0, -1));
            case "gH": //greenhome
                return new Tile(coordinate, Color.Green);
            case "b":
                return new Tile(coordinate, Color.Blue);
            case "bL":
                return new Tile(coordinate, Color.Blue, new Move(-1, 0));
            case "bH":
                return new Tile(coordinate, Color.Blue, new Move(+1, 0));
            case "bU":
                return new Tile(coordinate, Color.Blue, new Move(0, 1));
            case "bD":
                return new Tile(coordinate, Color.Blue, new Move(0, -1));
            case "bU-L":
                return new ArrowTile(coordinate, Color.Blue, new Move(-1, 0), new Move(0, 1));
            case "y":
                return new Tile(coordinate,Color.Yellow);
            case "yH":
                return new Tile(coordinate, Color.Yellow);
            case "yL": // yellow Left
                return new Tile(coordinate, Color.Yellow, new Move( -1, 0));
            case "yL-D": //yellow Left else Down
                return new ArrowTile(coordinate, Color.Yellow, new Move(0, -1), new Move(-1, 0));
            default:
                return new Tile(coordinate);
        }
    }
    
    //TODO FINISH map
    
    private readonly string[,] _standardBoard = new string[15, 15]
    {
        { "r", "r", "r", "r", "r", "r", "R", "gD-R", "D", "g", "g", "g", "g", "g", "g" },
        { "r", "", "", "", "", "r", "D", "gU", "gD", "g", "", "", "", "", "g" },
        { "r", "", "rH", "rH", "", "r", "U", "gD", "D", "g", "", "gH", "gH", "", "g" },
        { "r", "", "rH", "rH", "", "r", "U", "gD", "D", "g", "", "gH", "gH", "", "g" },
        { "r", "", "", "", "", "r", "U", "gD", "D", "g", "", "", "", "", "g" },
        { "r", "r", "r", "r", "r", "r", "U", "gD", "*", "g", "g", "g", "g", "g", "g" },
        { "R", "rR", "R", "R", "R", "*", "", "greenEnd", "", "*", "R", "R", "R", "R", "D" },
        { "rR-U", "rR", "rR", "rR", "rR", "rR", "redEnd", "", "yellowend", "yL", "yL", "yL", "yL", "yL", "yL-D" }, 
        { "U", "L", "L", "L", "L", "L", "", "blueEnd", "", "*", "L", "L", "L", "yL", "L" },
        { "b", "b", "b", "b", "b", "b", "*", "bU", "D", "y", "y", "y", "y", "y", "y" },
        { "b", "", "", "", "", "b", "U", "bU", "D", "y", "", "", "", "", "y" },
        { "b", "", "bH", "bH", "", "b", "U", "bU", "D", "y", "", "yH", "yH", "", "y" },
        { "b", "", "bH", "bH", "", "b", "U", "bU", "D", "y", "", "yH", "yH", "", "y" },
        { "b", "", "", "", "", "b", "bU", "bU", "D", "y", "", "", "", "", "y" },
        { "b", "b", "b", "b", "b", "b", "U", "bU-L", "L", "y", "y", "y", "y", "y", "y" },
    };
}