using System.Collections;
using LudoAPI.Models;
using LudoAPI.Models.Tiles;

namespace LudoTest.Services;

public class TestBoardMapsAndExpectedBoards : IEnumerable<object[]>
{
    private readonly List<object[]> _data =
    [
        new object[]
        {
            new string[4, 4]
            {
                { "r", "b", "g", "y" },
                { "UR", "DR", "DL", "UL" },
                { "rH", "bE-D", "gSD", "yL-D" },
                { "", "", "", "" }
            },
            new Dictionary<string, Tile>
            {
                { new Coordinate(0, 0).ToString(), new Tile(new Coordinate(0, 0), Color.Red) },
                { new Coordinate(1, 0).ToString(), new Tile(new Coordinate(1, 0), Color.Blue) },
                { new Coordinate(2, 0).ToString(), new Tile(new Coordinate(2, 0), Color.Green) },
                { new Coordinate(3, 0).ToString(), new Tile(new Coordinate(3, 0), Color.Yellow) },
                
                { new Coordinate(0, 1).ToString(), new Tile(new Coordinate(0, 1), new Move(+1, -1)) },
                { new Coordinate(1, 1).ToString(), new Tile(new Coordinate(1, 1), new Move(+1, +1)) },
                { new Coordinate(2, 1).ToString(), new Tile(new Coordinate(2, 1), new Move(-1, +1)) },
                { new Coordinate(3, 1).ToString(), new Tile(new Coordinate(3, 1), new Move(-1, -1)) },
                
                { new Coordinate(0, 2).ToString(), new HomeTile(new Coordinate(0, 2), Color.Red) },
                { new Coordinate(1, 2).ToString(), new EndTile(new Coordinate(1, 2), Color.Blue, new Move(0, +1)) },
                { new Coordinate(2, 2).ToString(), new StartTile(new Coordinate(2, 2), Color.Green, new Move(0, -1)) },
                {
                    new Coordinate(3, 2).ToString(),
                    new ArrowTile(new Coordinate(3, 2), Color.Yellow, new Move(-1, 0), new Move(0, -1))
                },
                
                { new Coordinate(0, 3).ToString(), new Tile(new Coordinate(0, 3)) },
                { new Coordinate(1, 3).ToString(), new Tile(new Coordinate(1, 3)) },
                { new Coordinate(2, 3).ToString(), new Tile(new Coordinate(2, 3)) },
                { new Coordinate(3, 3).ToString(), new Tile(new Coordinate(3, 3)) }
            }
        }
    ];

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}