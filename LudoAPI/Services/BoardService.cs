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
            case "rh": //redhome
                return new Tile(coordinate, Color.Red);
            case "rR": //redRight
                return new Tile(coordinate, Color.Red, new Move(1,0));
            case "rR-D": //redRight else Down
                return new ArrowTile(coordinate,Color.Red, new Move(0, -1), new Move(1,0));
            case "g":  //green
                return new Tile(coordinate, Color.Green);
            case "gU": //greenUp
                return new Tile(coordinate, Color.Green, new Move(0, 1));
            case "gD": //greenDown
                return new Tile(coordinate, Color.Green, new Move(0,-1));
            case "gD-L": //greenDown else Left
                return new ArrowTile(coordinate, Color.Green, new Move(-1, 0), new Move(0, -1));
            case "gh": //greenhome
                return new Tile(coordinate, Color.Green);
            case "yL": // yellow Left
                return new Tile(coordinate, Color.Yellow, new Move( -1, 0));
            case "yL-U": //yellow Left else Up
                return new ArrowTile(coordinate, Color.Yellow, new Move(0, 1), new Move(-1, 0));
            default:
                return new Tile(coordinate);
        }
    }
    
    //TODO FINISH map
    
    private readonly string[,] _standardBoard = new string[15, 15]
    {
        { "r", "r", "r", "r", "r", "r", "D", "gD-L", "L", "g", "g", "g", "g", "g", "g" },
        { "r", "", "", "", "", "r", "D", "gD", "gU", "g", "", "", "", "", "g" },
        { "r", "", "rh", "rh", "", "r", "D", "gD", "U", "g", "", "gh", "gh", "", "g" },
        { "r", "", "rh", "rh", "", "r", "D", "gD", "U", "g", "", "gh", "gh", "", "g" },
        { "r", "", "", "", "", "r", "D", "gD", "U", "g", "", "", "", "", "g" },
        { "r", "r", "r", "r", "r", "r", "*", "gD", "U", "g", "g", "g", "g", "g", "g" },
        { "L", "rL", "L", "L", "L", "L", "", "greenEnd", "", "*", "L", "L", "L", "L", "L" },
        { "rR-D", "rR", "rR", "rR", "rR", "rR", "redEnd", "", "yellowend", "yL", "yL", "yL", "yL", "yL", "yL-U" }, //TODO. Jeg er nået hertil
        { "*", "*", "*", "*", "*", "*", "", "bluehome", "", "*", "*", "*", "*", "*", "*" },
        { "", "", "", "", "", "", "*", "blue", "*", "", "", "", "", "", "" },
        { "", "", "", "", "", "", "*", "blue", "*", "", "", "", "", "", "" },
        { "", "", "blue", "blue", "", "", "*", "blue", "*", "", "", "yellow", "yellow", "", "" },
        { "", "", "blue", "blue", "", "", "*", "blue", "*", "", "", "yellow", "yellow", "", "" },
        { "", "", "", "", "", "", "*", "blue", "*", "", "", "", "", "", "" },
        { "", "", "", "", "", "", "*", "blueup", "*", "", "", "", "", "", "" },
    };
}