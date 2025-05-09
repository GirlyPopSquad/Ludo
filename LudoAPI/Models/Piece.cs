namespace LudoAPI.Models
{
    public class Piece
    {
        public int PieceNumber { get; }
        public Color Color { get; }
        public Coordinate Coordinate { get; }

        public Piece(int pieceNumber, Color color, Coordinate coordinate)
        {
            Color = color;
            PieceNumber = pieceNumber;
            Coordinate = coordinate;
        }
    }
}