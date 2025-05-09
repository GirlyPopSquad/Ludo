namespace LudoAPI.Models;

public class Game
{
    public int Id {  get; }
    public List<Player> Players { get; }
    public Color CurrentPlayerColor {get; set;}
    
    public Game(List<Player> players, Color currentPlayerColor)
    {
        Players = players;
        CurrentPlayerColor = currentPlayerColor;
    }

    public Game (int id, Game game)
    {
        Id = id;
        Players = game.Players;
        CurrentPlayerColor = game.CurrentPlayerColor;
    }
}
    