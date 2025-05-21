using System.Text.Json.Serialization;

namespace LudoAPI.Models
{
    public class Roll
    {
        public int PlayerId { get; }

        public int Value { get; set; }

        [JsonConstructor]
        public Roll(int playerId, int value)
        {
            PlayerId = playerId;
            Value = value;
        }
    }
}
