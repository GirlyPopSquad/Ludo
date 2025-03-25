namespace LudoAPI.Services
{
    public interface IDiceService
    {
        int RollDice();

        bool IsItA6(int i);
    }
}