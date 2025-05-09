using FluentAssertions;
using LudoAPI.Models;
using LudoAPI.Models.Tiles;
using LudoAPI.Repositories;
using LudoAPI.Services;
using Moq;

namespace LudoTest.Services;

public class BoardServiceTest
{
    private readonly Mock<IBoardRepository> _boardRepositoryMock = new();
    private readonly BoardService _service;

    public BoardServiceTest()
    {
        _service = new BoardService(_boardRepositoryMock.Object);
    }


    [Fact]
    public void InitStandardBoard_CreatesAStandardBoardAsExpected()
    {
        //arrange
        var gameId = -1;
        
        //act
        var result = _service.InitStandardBoard(gameId);

        //assert
        result.Should().Be(1);
        
        _boardRepositoryMock.Verify(x => x.Add(It.Is<Board>(b =>
            b.GameId == -1
            && b.Rows == 15
            && b.Cols == 15
        )), Times.Once);
    }

    [Theory]
    [ClassData(typeof(TestBoardMapsAndExpectedBoards))]
    public void MakeBoardFromMap_CreatesExpectedBoard(string[,] testMap, Dictionary<string, Tile> expectedTiles)
    {
        //act
        var tiles = _service.MakeTilesFromMap(testMap);

        //assert
        tiles.Should().NotBeNull();
        tiles.Values.Should().HaveCount(16);
        tiles.Should().BeEquivalentTo(expectedTiles);
    }
}