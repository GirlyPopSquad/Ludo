namespace LudoAPI.Models
{
    public class Roll
    {
        public LobbyPlayer Player { get; }

        public int Value { get; }

        public Roll(LobbyPlayer _player, int _value)
        {
            this.Player = _player;
            this.Value = _value;
        }
    }
}
