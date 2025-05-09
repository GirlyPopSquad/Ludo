namespace LudoAPI.Models;

public class Game
{
    public int Id {  get; }
    //todo could be private if not for test?
    public List<GamePlayer> Players { get; }
    public int? CurrentPlayerId {get; set;}

    //todo board/tiles
    
    public Game(List<GamePlayer> players, int? currentPlayerId)
    {
        this.Players = players;
        this.CurrentPlayerId = currentPlayerId;
    }

    public Game (int id, Game game)
    {
        Id = id;
        Players = game.Players;
        CurrentPlayerId = game.CurrentPlayerId;
    }
}
    