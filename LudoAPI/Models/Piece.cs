namespace LudoAPI.Models
{
    public class Piece
    {
        public Guid Id { get; } = Guid.NewGuid();
        public Tile Tile { get; set; }
        public int PlayerId { get; }
        
        public Piece(Tile tile, int playerId)
        {
            Tile = tile;
            PlayerId = playerId;
        }
    }
}
