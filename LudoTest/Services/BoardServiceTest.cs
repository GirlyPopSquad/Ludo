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

    [Theory]
    [ClassData(typeof(TestBoardMapsAndExpectedBoards))]
    public void MakeBoardFromMap_CreatesExpectedBoard(string[,] testMap, Dictionary<string, Tile> expectedTiles)
    {
        //act
        var board = service.MakeBoardFromMap(-1, testMap);

        //assert
        board.Should().NotBeNull();
        board.GameId.Should().Be(-1);
        board.Rows.Should().Be(4);
        board.Cols.Should().Be(4);
        board.Tiles.Values.Should().HaveCount(16);
        board.Tiles.Should().BeEquivalentTo(expectedTiles);
    }
}