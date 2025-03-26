namespace LudoAPI.Models;

public class Lobby
{
    
    public int Id { get; set; }
    
    public List<LobbyPlayer> Players { get; set; } 

    public List<Roll> Rolls { get; set; } = new List<Roll>();

    public Lobby() { }
    
    public Lobby(List<LobbyPlayer> players, int id)
    {
        Players = players;
        Id = id;
    }

 
}