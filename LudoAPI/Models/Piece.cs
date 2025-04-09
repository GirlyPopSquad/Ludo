namespace LudoAPI.Models
{
    public class Piece
    {
        public int Id { get; }
        // will probably need a playerID or some sort of reference to its player

        public Piece(int id)
        {
            Id = id;
        }
    }
}
