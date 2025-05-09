using System.Text.Json.Serialization;

namespace LudoAPI.Models.Tiles;

[JsonDerivedType(typeof(Tile), nameof(Tile))]
[JsonDerivedType(typeof(ArrowTile), nameof(ArrowTile))]
[JsonDerivedType(typeof(StartTile), nameof(StartTile))]
[JsonDerivedType(typeof(HomeTile), nameof(HomeTile))]
[JsonDerivedType(typeof(EndTile), nameof(EndTile))]
public class Tile
{
    public Coordinate Coordinate { get; }
    internal readonly Move? Move;
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
        this.Move = move;
    }

    public Tile(Coordinate coordinate, Color color, Move move)
    {
        this.Move = move;
        Color = color;
        Coordinate = coordinate;
    }


    public virtual Coordinate NextCoordinate(Piece piece)
    {
        if (Move == null) return piece.Coordinate;
        
        var nextCoordinate = piece.Coordinate.CalcNextCoordinateFromMove(Move);
        
        return nextCoordinate;
    }
}