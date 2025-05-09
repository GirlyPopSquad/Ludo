namespace LudoAPI.Models
{
    public class Roll
    {
        public Player Player { get; set; }

        public int Value { get; set; }

        public Roll(Player player, int value)
        {
            this.Player = player;
            this.Value = value;
        }
    }
}
