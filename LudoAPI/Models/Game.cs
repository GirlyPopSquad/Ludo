namespace LudoAPI.Models;

public class Game
{
    public int Id {  get; }
    public List<Player> Players { get; }
    //Is initially decided by who rolls the highest number on the dice
    public int? CurrentPlayerId {get; set;}


    public Game(int id, List<Player> players, int? currentPlayerId)
    {
        Id = id;
        Players = players;
        CurrentPlayerId = currentPlayerId;
    }
}
    