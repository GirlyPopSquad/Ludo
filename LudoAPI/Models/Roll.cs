namespace LudoAPI.Models
{
    public class Roll
    {
        public LobbyPlayer Player { get; set; }

        public int Value { get; set; }

        public Roll(LobbyPlayer player, int value)
        {
            this.Player = player;
            this.Value = value;
        }
    }
}
