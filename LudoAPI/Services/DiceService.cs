namespace LudoAPI.Services;

public class DiceService : IDiceService
{
    private readonly Random _random = new();

    public int RollDice()
    {
        return _random.Next(1, 7);
    }
}