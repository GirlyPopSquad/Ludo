namespace LudoAPI.Models;

public class Move
{
    public int XChange { get; }
    public int YChange { get; }

    public Move(int xChange, int yChange)
    {
        XChange = xChange;
        YChange = yChange;
    }
}