using System.Text.Json.Serialization;

namespace LudoAPI.Models.Tiles;

[JsonDerivedType(typeof(Tile), nameof(Tile))]
[JsonDerivedType(typeof(ArrowTile), nameof(ArrowTile))]
[JsonDerivedType(typeof(StartTile), nameof(StartTile))]
[JsonDerivedType(typeof(HomeTile), nameof(HomeTile))]
public class Tile
{
    public Coordinate Coordinate { get; }
    private Move? move;
    public Color? Color { get; }

    public Tile(Coordinate coordinate)
    {
        Coordinate = coordinate;
    }
    
    public Tile(Coordinate coordinate, Color color)
    {
        Coordinate = coordinate;
        Color = color;
    }

    public Tile(Coordinate coordinate, Move move)
    {
        Coordinate = coordinate;
        this.move = move;
    }

    public Tile(Coordinate coordinate, Color color, Move move)
    {
        this.move = move;
        Color = color;
        Coordinate = coordinate;
    }


    public virtual Move NextMove(Piece piece)
    {
        throw new NotImplementedException();
    }
}