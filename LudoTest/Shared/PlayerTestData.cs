using LudoAPI.Models;

namespace LudoTest.Shared;

public static class PlayerTestData
{
    public static readonly Player RedPlayer = new(Color.Red);
    public static readonly Player GreenPlayer = new(Color.Green);
    public static readonly Player YellowPlayer = new(Color.Yellow);
    public static readonly Player BluePlayer = new(Color.Blue);


    public static List<Player> Get4Players()
    {
        return [RedPlayer, GreenPlayer, YellowPlayer, BluePlayer];
    }
    
}