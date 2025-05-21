namespace LudoAPI.Models.Tiles;

public class EndPathTile : Tile
{
    private readonly Move _backMove;
    
    public EndPathTile(Coordinate coordinate, Color color, Move move, Move backMove) : base(coordinate, color, move)
    {
        _backMove = backMove;
    }

    public Coordinate FormerCoordinate()
    {
        return Coordinate.CalcNextCoordinateFromMove(_backMove);
    }
}