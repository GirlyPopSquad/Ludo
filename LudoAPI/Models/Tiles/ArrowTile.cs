namespace LudoAPI.Models.Tiles;

//TODO find out how to make this recognisable from FE - so that it can look like an arrow

public class ArrowTile : Tile
{
    
    private Move arrowMove;
    
    
    public ArrowTile(Coordinate coordinate, Color color, Move move, Move arrowMove) : base(coordinate, color, move)
    {
        this.arrowMove = arrowMove;
    }


    public override Move NextMove(Piece piece)
    {
        throw new NotImplementedException();
    }
}