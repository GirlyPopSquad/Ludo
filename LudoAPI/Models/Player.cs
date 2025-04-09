namespace LudoAPI.Models;

public class Player
{
    public Player(int id, List<Piece> pieces)
    {
        Id = id;
        Pieces = pieces;
    }

    public int Id { get; }
    public List<Piece> Pieces { get; set; }
    
}