﻿using System.Text.Json.Serialization;

namespace LudoAPI.Models.Tiles;

[JsonDerivedType(typeof(Tile), nameof(Tile))]
[JsonDerivedType(typeof(ArrowTile), nameof(ArrowTile))]
[JsonDerivedType(typeof(StartTile), nameof(StartTile))]
[JsonDerivedType(typeof(HomeTile), nameof(HomeTile))]
[JsonDerivedType(typeof(EndTile), nameof(EndTile))]
[JsonDerivedType(typeof(EndPathTile), nameof(EndPathTile))]
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
        Move = move;
    }

    public Tile(Coordinate coordinate, Color color, Move move)
    {
        Move = move;
        Color = color;
        Coordinate = coordinate;
    }


    public virtual Coordinate NextCoordinate(Piece piece)
    {
        if (Move == null) return Coordinate;
        
        var nextCoordinate = Coordinate.CalcNextCoordinateFromMove(Move);
        
        return nextCoordinate;
    }
}