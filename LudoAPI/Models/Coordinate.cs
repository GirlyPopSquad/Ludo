namespace LudoAPI.Models;

public record Coordinate(int X, int Y)
{
    public override int GetHashCode()
    {
        return X.GetHashCode() ^ Y.GetHashCode();
    }

    public virtual bool Equals(Coordinate? other)
    {
        if (other is null)
        {
            return false;
        }
        
        return X == other.X && Y == other.Y;
    }

    public override string ToString()
    {
        return $"{X},{Y}";
    }
    
    public Coordinate CalcNextCoordinateFromMove(Move move)
    {
        var nextX = X + move.XChange;
        var nextY = Y + move.YChange;

        return new Coordinate(nextX, nextY);
    }
}