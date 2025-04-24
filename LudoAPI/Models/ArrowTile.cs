namespace LudoAPI.Models;

public class ArrowTile : Tile
{
    
    public Coordinate Coordinate { get; }
    private Move arrowMove;
    public Color? Color;
    //todo arrow direction
    
    
    public ArrowTile(Coordinate coordinate, Color color, Move move, Move arrowMove) : base(coordinate, color, move)
    {
        this.arrowMove = arrowMove;
        Color = color;
        Coordinate = coordinate;
    }


    public override Move nextMove(Piece piece)
    {
        throw new NotImplementedException();
    }
}