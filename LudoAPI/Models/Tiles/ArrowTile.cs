namespace LudoAPI.Models.Tiles;

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