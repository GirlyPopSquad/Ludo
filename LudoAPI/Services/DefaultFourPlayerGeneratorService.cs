using LudoAPI.Models;

namespace LudoAPI.Services
{

    public class DefaultFourPlayerGeneratorService : IPlayerGenerator
    {
        public List<Player> GeneratePlayers()
        {
            return new List<Player>
            {
                new((Color)1),
                new((Color)2),
                new((Color)3),
                new((Color)4),
            };
        }
    }
}
