using LudoAPI.Models;

namespace LudoAPI.Services
{
    public interface IPlayerGenerator
    {
        List<Player> GeneratePlayers();
    }
}
