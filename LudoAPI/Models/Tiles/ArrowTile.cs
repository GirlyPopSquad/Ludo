namespace LudoAPI.Models.Tiles;

public class ArrowTile : Tile
{
    private Move arrowMove;

    //used in UI
    public ArrowDirection ArrowDirection { get; }


    public ArrowTile(Coordinate coordinate, Color color, Move move, Move arrowMove) : base(coordinate, color, move)
    {
        this.arrowMove = arrowMove;
        ArrowDirection = CalculateArrowDirection();
    }

    private ArrowDirection CalculateArrowDirection()
    {
        var yChange = arrowMove.YChange;

        // todo make prettier
        switch (arrowMove.XChange)
        {
            case -1:
                if (yChange == 0) return ArrowDirection.Left;
                break;
            case 1:
                if (yChange == 0) return ArrowDirection.Right;
                break;
            case 0:
                switch (yChange)
                {
                    case 1:
                        return ArrowDirection.Up;
                    case -1:
                        return ArrowDirection.Down;
                }

                break;
        }

        throw new NotImplementedException("ArrowDirection is not supported");
    }

    public override Move NextMove(Piece piece)
    {
        throw new NotImplementedException();
    }
}

public enum ArrowDirection
{
    Up,
    Right,
    Down,
    Left,
}