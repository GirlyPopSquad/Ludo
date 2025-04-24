namespace LudoAPI.Models;

public class Game
{
    public int Id {  get; }
    //todo could be private if not for test?
    public List<LobbyPlayer> Players { get; }
    //Is initially decided by who rolls the highest number on the dice
    public int? CurrentPlayerId {get; set;}

    //todo board/tiles
    
    public Game(List<LobbyPlayer> players, int currentPlayerId)
    {
        this.Players = players;
        this.CurrentPlayerId = currentPlayerId;
    }
}
    