namespace LudoAPI.Models;

public class Player
{
    public int Id {  get; }
    public Color Color {  get; }

    public Player(Color color)
    {
        Id = (int) color;
        Color = color;
    }
}