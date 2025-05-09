namespace LudoAPI.Models;

public record Coordinate(int X, int Y)
{
    public override int GetHashCode()
    {
        return X.GetHashCode() ^ Y.GetHashCode();
    }

    public override string ToString()
    {
        return $"{X},{Y}";
    }
}