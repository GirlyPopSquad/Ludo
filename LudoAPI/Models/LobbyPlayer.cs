namespace LudoAPI.Models
{
    public class LobbyPlayer
    {
        // number from 1-4
        public int Id { get; }

        public LobbyPlayer(int id)
        {
            Id = id;
        }
    }
}
