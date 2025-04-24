using LudoAPI.Models;

namespace LudoAPI.Services;

public class BoardService : IBoardService
{
    public Board InitStandardBoard(int gameId)
    {
        var schema = standardBoard2;
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

    private Tile TranslateToTile(string tileString, Coordinate coordinate)
    {
        switch (tileString)
        {
            case "":
                return new Tile(coordinate);
            case "L":
                return new Tile(coordinate, new Move(0, -1));
            case "D":
                return new Tile(coordinate, new Move(0, -1));
            case "r":
                return new Tile(coordinate,Color.Red);
            case "redhome":
                return new Tile(coordinate, Color.Red);
            case "redRight":
                return new Tile(coordinate, Color.Red, new Move(1,0));
            case "g":
                return new Tile(coordinate, Color.Green);
            case "gD":
                return new ArrowTile(coordinate, Color.Green, new Move(-1, 0), new Move(0, -1));
            
                
            default:
                return new Tile(coordinate);
        }
    }
    
    private string[,] standardBoard2 = new string[15, 15]
    {
        { "r", "r", "r", "r", "r", "r", "D", "gD", "L", "g", "g", "g", "g", "g", "g" },
        { "", "", "", "", "", "", "*", "green", "*", "", "", "", "", "", "" },
        { "", "", "redhome", "redhome", "", "", "*", "green", "*", "", "", "green", "green", "", "" },
        { "", "", "redhome", "redhome", "", "", "*", "green", "*", "", "", "green", "green", "", "" },
        { "", "", "", "", "", "", "*", "green", "*", "", "", "", "", "", "" },
        { "", "", "", "", "", "", "*", "green", "*", "", "", "", "", "", "" },
        { "*", "*", "*", "*", "*", "*", "", "greenend", "", "*", "*", "*", "*", "*", "*" },
        { "redRightArrow", "redRight", "redRight", "redRight", "redRight", "redRight", "redEnd", "", "yellowend", "yellow", "yellow", "yellow", "yellow", "yellow", "yellowleft" },
        { "*", "*", "*", "*", "*", "*", "", "bluehome", "", "*", "*", "*", "*", "*", "*" },
        { "", "", "", "", "", "", "*", "blue", "*", "", "", "", "", "", "" },
        { "", "", "", "", "", "", "*", "blue", "*", "", "", "", "", "", "" },
        { "", "", "blue", "blue", "", "", "*", "blue", "*", "", "", "yellow", "yellow", "", "" },
        { "", "", "blue", "blue", "", "", "*", "blue", "*", "", "", "yellow", "yellow", "", "" },
        { "", "", "", "", "", "", "*", "blue", "*", "", "", "", "", "", "" },
        { "", "", "", "", "", "", "*", "blueup", "*", "", "", "", "", "", "" },
    };

    private string[,] standardBoard = new string[15, 15]
    {
        { "", "", "", "", "", "", "*", "greendown", "*", "", "", "", "", "", "" },
        { "", "", "", "", "", "", "*", "green", "*", "", "", "", "", "", "" },
        { "", "", "redhome", "redhome", "", "", "*", "green", "*", "", "", "green", "green", "", "" },
        { "", "", "redhome", "redhome", "", "", "*", "green", "*", "", "", "green", "green", "", "" },
        { "", "", "", "", "", "", "*", "green", "*", "", "", "", "", "", "" },
        { "", "", "", "", "", "", "*", "green", "*", "", "", "", "", "", "" },
        { "*", "*", "*", "*", "*", "*", "", "greenend", "", "*", "*", "*", "*", "*", "*" },
        { "redRightArrow", "redRight", "redRight", "redRight", "redRight", "redRight", "redEnd", "", "yellowend", "yellow", "yellow", "yellow", "yellow", "yellow", "yellowleft" },
        { "*", "*", "*", "*", "*", "*", "", "bluehome", "", "*", "*", "*", "*", "*", "*" },
        { "", "", "", "", "", "", "*", "blue", "*", "", "", "", "", "", "" },
        { "", "", "", "", "", "", "*", "blue", "*", "", "", "", "", "", "" },
        { "", "", "blue", "blue", "", "", "*", "blue", "*", "", "", "yellow", "yellow", "", "" },
        { "", "", "blue", "blue", "", "", "*", "blue", "*", "", "", "yellow", "yellow", "", "" },
        { "", "", "", "", "", "", "*", "blue", "*", "", "", "", "", "", "" },
        { "", "", "", "", "", "", "*", "blueup", "*", "", "", "", "", "", "" },
    };
}