using FluentAssertions;
using LudoAPI.Models;
using LudoAPI.Models.Tiles;
using LudoAPI.Services;

namespace LudoTest.Services;

public class BoardServiceTest
{
    private BoardService service = new BoardService();


    [Fact]
    public void InitStandardBoard_CreatesAStandardBoardAsExpected()
    {
        //arrange
        var gameId = -1;

        //act
        var result = service.InitStandardBoard(gameId);

        //assert
        result.Should().BeOfType<Board>();
        result.GameId.Should().Be(gameId);
        result.Rows.Should().Be(15);
        result.Cols.Should().Be(15);
    }

    [Fact]
    public void MakeBoardFromMap_CreatesExpectedBoard()
    {
        //arrange

        //todo move data to theory
        string[,] testmap = new string[4, 4]
        {
            { "r", "b", "g", "y" },
            { "rR", "bU", "gD", "yL" },
            { "rH", "bE", "gSD", "yL-D" },
            { "", "", "", "" }
        };

        Dictionary<string, Tile> expectedTiles = [];

        expectedTiles.Add(new Coordinate(0, 0).ToString(), new Tile(new Coordinate(0, 0), Color.Red));
        expectedTiles.Add(new Coordinate(1, 0).ToString(), new Tile(new Coordinate(1, 0), Color.Blue));
        expectedTiles.Add(new Coordinate(2, 0).ToString(), new Tile(new Coordinate(2, 0), Color.Green));
        expectedTiles.Add(new Coordinate(3, 0).ToString(), new Tile(new Coordinate(3, 0), Color.Yellow));

        expectedTiles.Add(new Coordinate(0, 1).ToString(), new Tile(new Coordinate(0, 1), Color.Red, new Move(+1, 0)));
        expectedTiles.Add(new Coordinate(1, 1).ToString(), new Tile(new Coordinate(1, 1), Color.Blue, new Move(0, 1)));
        expectedTiles.Add(new Coordinate(2, 1).ToString(), new Tile(new Coordinate(2, 1), Color.Green, new Move(0, -1)));
        expectedTiles.Add(new Coordinate(3, 1).ToString(),
            new Tile(new Coordinate(3, 1), Color.Yellow, new Move(-1, 0)));

        expectedTiles.Add(new Coordinate(0, 2).ToString(), new HomeTile(new Coordinate(0, 2), Color.Red));
        expectedTiles.Add(new Coordinate(1, 2).ToString(), new EndTile(new Coordinate(1, 2), Color.Blue));
        expectedTiles.Add(new Coordinate(2, 2).ToString(),
            new StartTile(new Coordinate(2, 2), Color.Green, new Move(0, -1)));
        expectedTiles.Add(new Coordinate(3, 2).ToString(),
            new ArrowTile(new Coordinate(3, 2), Color.Yellow, new Move(-1, 0), new Move(0, -1)));

        for (var x = 0; x < 4; x++)
        {
            var cord = new Coordinate(x, 3);
            expectedTiles.Add(cord.ToString(), new Tile(cord));
        }

        //act
        var board = service.MakeBoardFromMap(-1, testmap);

        //assert
        board.Should().NotBeNull();
        board.GameId.Should().Be(-1);
        board.Rows.Should().Be(4);
        board.Cols.Should().Be(4);
        board.Tiles.Values.Should().HaveCount(16);
        board.Tiles.Should().BeEquivalentTo(expectedTiles);
    }
}