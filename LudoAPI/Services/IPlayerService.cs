using LudoAPI.Models;

namespace LudoAPI.Services
{
    public interface IPlayerService
    {
        public IReadOnlyList<Player> Players { get; }
        void AddPlayer();
        Color ChooseColor(Color color);
    }
}