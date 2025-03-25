namespace LudoAPI.Models;

public class Game
{
    public int Id {  get; }
    //todo could be private if not for test?
    public List<LobbyPlayer> players { get; }
    //Is initially decided by who rolls the highest number on the dice
    public int? currentPlayerId {get; set;}

    //todo board/tiles
    
    public Game(List<LobbyPlayer> players, int currentPlayerId)
    {
        this.players = players;
        this.currentPlayerId = currentPlayerId;
    }
}
    