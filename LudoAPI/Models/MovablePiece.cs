namespace LudoAPI.Models;

//map between piece and potential next coordinate
public class MovablePiece
{
    public int PieceNumber { get; set; }
    public Coordinate PotentialCoordinate { get; set; }


    public MovablePiece(int pieceNumber, Coordinate potentialCoordinate)
    {
        PieceNumber = pieceNumber;
        PotentialCoordinate = potentialCoordinate;
    }
}