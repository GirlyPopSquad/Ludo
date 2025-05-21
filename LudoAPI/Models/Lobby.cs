namespace LudoAPI.Models;

public class Lobby
{
    public int Id { get; set; }
    
    public List<Player> Players { get; set; } 

    public List<Roll> Rolls { get; set; } = new List<Roll>();
    
    public Lobby(int id, List<Player> players)
    {
        Players = players;
        Id = id;
    }
}