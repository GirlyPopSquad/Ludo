namespace LudoAPI.Models;

public class Move
{
    private int XChange { get; }
    private int YChange { get; }

    public Move(int xChange, int yChange)
    {
        XChange = xChange;
        YChange = yChange;
    }
}