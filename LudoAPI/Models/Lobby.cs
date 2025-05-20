namespace LudoAPI.Models;

public class Lobby
{
    public int Id { get; }
    
    public List<Player> Players { get; set; } 

    public List<Roll> Rolls { get; set; } = [];
    
    public Lobby(int id, List<Player> players)
    {
        Players = players;
        Id = id;
    }
}