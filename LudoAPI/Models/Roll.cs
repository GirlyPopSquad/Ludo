namespace LudoAPI.Models
{
    public class Roll
    {
        public LobbyPlayer Player { get; set; }

        public int Value { get; set; }

        public Roll() { }
        public Roll(LobbyPlayer _player, int _value)
        {
            this.Player = _player;
            this.Value = _value;
        }
    }
}
