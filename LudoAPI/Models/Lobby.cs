namespace LudoAPI.Models;

public class Lobby
{
    
    public int Id { get; }
    
    public List<LobbyPlayer> Players { get; } 

    public List<Roll> StartingRolls { get; set; } = new List<Roll>();
    
    public Lobby(List<LobbyPlayer> players, int id)
    {
        Players = players;
        Id = id;
    }

 
}