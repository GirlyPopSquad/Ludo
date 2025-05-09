namespace LudoAPI.Models.Tiles;

public class HomeTile : Tile
{
    public StartTile[] StartTiles { get; set; } = [];

    public HomeTile(Coordinate coordinate, Color color) : base(coordinate, color)
    {
        
    }

    public Coordinate[] GetStartCoordinates()
    {
        return StartTiles.Select(x => x.Coordinate).ToArray();
    }
}