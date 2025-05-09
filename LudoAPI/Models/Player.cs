namespace LudoAPI.Models
{
    public class Player
    {
        // number from 1-4
        public Color Color { get; set; }

        public Player(Color color)
        {
            Color = color;
        }

        public Player(int colorValue)
        {
            if (!Enum.IsDefined(typeof(Color), colorValue))
                throw new ArgumentOutOfRangeException(nameof(colorValue));

            Color = (Color)colorValue;
        }
    }
}
