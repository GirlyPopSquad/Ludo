using System.Text.Json.Serialization;
using LudoAPI.Models.Tiles;

namespace LudoAPI.Models;

[JsonDerivedType(typeof(ArrowTile), typeof(HomeTile), typeof(StartTile)]
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