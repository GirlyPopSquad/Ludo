using System.Text.Json.Serialization;

namespace LudoAPI.Models
{
    public class Roll
    {
        public int PlayerId { get; set; }

        public int Value { get; set; }

        public Roll(Player player, int value)
        {
            PlayerId = player.Id;
            Value = value;
        }

        [JsonConstructor]
        public Roll(int playerId, int value)
        {
            PlayerId = playerId;
            Value = value;
        }
    }
}
