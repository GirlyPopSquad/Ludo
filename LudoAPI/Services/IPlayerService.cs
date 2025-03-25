using LudoAPI.Models;

namespace LudoAPI.Services
{
    public interface IPlayerService
    {
        public IReadOnlyList<LobbyPlayer> Players { get; }
        void AddPlayer();
        Color ChooseColor(Color color);
    }
}