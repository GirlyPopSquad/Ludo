namespace LudoAPI.Models;

public class GamePlayer
{
    public int Id {  get; }
    public Piece[] Pieces { get; }

    public GamePlayer(int id, Piece[] pieces)
    {
        Id = id;
        Pieces = pieces;
    }
}