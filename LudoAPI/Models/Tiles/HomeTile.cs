namespace LudoAPI.Models.Tiles;

public class HomeTile : Tile
{
    public StartTile[] StartTiles { get; set; } = [];

    public HomeTile(Coordinate coordinate, Color color) : base(coordinate, color)
    {
        
    }

    public override Coordinate NextCoordinate(Piece piece)
    {
        var startCoordinate = StartTiles.First().Coordinate;
        
        return startCoordinate;
    }

    public Coordinate[] GetStartCoordinates()
    {
        return StartTiles.Select(x => x.Coordinate).ToArray();
    }
}