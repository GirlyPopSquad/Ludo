namespace LudoAPI.Models
{
    public class LobbyPlayer
    {
        // number from 1-4
        public int Id { get; set; }

        public LobbyPlayer() { }
        public LobbyPlayer(int id)
        {
            Id = id;
        }
    }
}
