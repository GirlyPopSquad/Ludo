using LudoAPI.Models;
using LudoAPI.Models.Tiles;

namespace LudoAPI.Services.Utils;

public class BoardMapLibrary
{
    public static readonly string[,] StandardBoard = new string[15, 15]
    {
        { "r", "r", "r", "r", "r", "r", "R", "gD-R", "D", "g", "g", "g", "g", "g", "g" },
        { "r", "", "", "", "", "r", "U", "gEp-D", "gSD", "g", "", "", "", "", "g" },
        { "r", "", "rH", "rH", "", "r", "U", "gEp-D", "D", "g", "", "gH", "gH", "", "g" },
        { "r", "", "rH", "rH", "", "r", "U", "gEp-D", "D", "g", "", "gH", "gH", "", "g" },
        { "r", "", "", "", "", "r", "U", "gEp-D", "D", "g", "", "", "", "", "g" },
        { "r", "r", "r", "r", "r", "r", "U", "gEp-D", "DR", "g", "g", "g", "g", "g", "g" },
        { "R", "rSR", "R", "R", "R", "UR", "", "gE-U", "", "R", "R", "R", "R", "R", "D" },
        { "rR-U", "rEp-R", "rEp-R", "rEp-R", "rEp-R", "rEp-R", "rE-L", "", "yE-R", "yEp-L", "yEp-L", "yEp-L", "yEp-L", "yEp-L", "yL-D" },
        { "U", "L", "L", "L", "L", "L", "", "bE-D", "", "DL", "L", "L", "L", "ySL", "L" },
        { "b", "b", "b", "b", "b", "b", "UL", "bEp-U", "D", "y", "y", "y", "y", "y", "y" },
        { "b", "", "", "", "", "b", "U", "bEp-U", "D", "y", "", "", "", "", "y" },
        { "b", "", "bH", "bH", "", "b", "U", "bEp-U", "D", "y", "", "yH", "yH", "", "y" },
        { "b", "", "bH", "bH", "", "b", "U", "bEp-U", "D", "y", "", "yH", "yH", "", "y" },
        { "b", "", "", "", "", "b", "bSU", "bEp-U", "D", "y", "", "", "", "", "y" },
        { "b", "b", "b", "b", "b", "b", "U", "bU-L", "L", "y", "y", "y", "y", "y", "y" },
    };

    //Up/Down is opposite of what you would think,
    public static Tile TranslateToTile(string tileString, Coordinate coordinate)
    {
        return tileString switch
        {
            "" => new Tile(coordinate),
            "L" => //left
                new Tile(coordinate, new Move(-1, 0)),
            "R" => //right
                new Tile(coordinate, new Move(+1, 0)),
            "U" => //up
                new Tile(coordinate, new Move(0, -1)),
            "D" => //down
                new Tile(coordinate, new Move(0, +1)),
            "UL" => //Up-Left
                new Tile(coordinate, new Move(-1, -1)),
            "UR" => //Up-Right
                new Tile(coordinate, new Move(1, -1)),
            "DL" => //Down-Left
                new Tile(coordinate, new Move(-1, +1)),
            "DR" => //Down-Right:
                new Tile(coordinate, new Move(1, +1)),
            "r" => //red
                new Tile(coordinate, Color.Red),
            "rL" => //redLeft
                new Tile(coordinate, Color.Red, new Move(-1, 0)),
            "rH" => //redHome
                new HomeTile(coordinate, Color.Red),
            "rE-L" => //redEnd
                new EndTile(coordinate, Color.Red, new Move(-1, 0)),
            "rSR" => //red Start Right
                new StartTile(coordinate, Color.Red, new Move(1, 0)),
            "rEp-R" => //red Endpath Right
                new EndPathTile(coordinate, Color.Red, new Move(1, 0), new Move(-1, 0)),
            "rR-U" => //redRight else UP
                new ArrowTile(coordinate, Color.Red, new Move(0, -1), new Move(1, 0)),
            "g" => //green
                new Tile(coordinate, Color.Green),
            "gU" => //greenUp
                new Tile(coordinate, Color.Green, new Move(0, -1)),
            "gEp-D" => //green Endpath Down
                new EndPathTile(coordinate, Color.Green, new Move(0, +1), new Move(0, -1)),
            "gD-R" => //greenDown else Right
                new ArrowTile(coordinate, Color.Green, new Move(1, 0), new Move(0, +1)),
            "gH" => //greenHome
                new HomeTile(coordinate, Color.Green),
            "gE-U" => new EndTile(coordinate, Color.Green, new Move(0, -1)),
            "gSD" => new StartTile(coordinate, Color.Green, new Move(0, +1)),
            "b" => new Tile(coordinate, Color.Blue),
            "bL" => new Tile(coordinate, Color.Blue, new Move(-1, 0)),
            "bH" => new HomeTile(coordinate, Color.Blue),
            "bE-D" => new EndTile(coordinate, Color.Blue, new Move(0, +1)),
            "bSU" => new StartTile(coordinate, Color.Blue, new Move(0, -1)),
            "bEp-U" => new EndPathTile(coordinate, Color.Blue, new Move(0, -1), new Move(0, +1)),
            "bD" => new Tile(coordinate, Color.Blue, new Move(0, 1)),
            "bU-L" => new ArrowTile(coordinate, Color.Blue, new Move(-1, 0), new Move(0, -1)),
            "y" => new Tile(coordinate, Color.Yellow),
            "yH" => new HomeTile(coordinate, Color.Yellow),
            "yE-R" => new EndTile(coordinate, Color.Yellow, new Move(1, 0)),
            "ySL" => new StartTile(coordinate, Color.Yellow, new Move(-1, 0)),
            "yEp-L" => new EndPathTile(coordinate, Color.Yellow, new Move(-1, 0), new Move(+1, 0)),
            "yL-D" => //yellow Left else Down
                new ArrowTile(coordinate, Color.Yellow, new Move(0, +1), new Move(-1, 0)),
            _ => new Tile(coordinate)
        };
    }
}