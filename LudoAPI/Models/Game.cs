namespace LudoAPI.Models;

public class Game
{
    public int Id { get; }
    public List<Player> Players { get; }
    public int CurrentPlayerId { get; set; }
    public bool TimeToRoll { get; set; } = true;
    public List<int> FinishedPlayerIds { get; } = [];

    public Game(List<Player> players, int currentPlayerId)
    {
        Players = players;
        CurrentPlayerId = currentPlayerId;
    }

    public Game(int id, Game game)
    {
        Id = id;
        Players = game.Players;
        CurrentPlayerId = game.CurrentPlayerId;
    }
}