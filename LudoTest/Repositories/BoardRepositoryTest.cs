using FluentAssertions;
using LudoAPI.Models;
using LudoAPI.Models.Tiles;
using LudoAPI.Repositories;
using LudoTest.Services;

namespace LudoTest.Repositories;

public class BoardRepositoryTest
{
    private readonly BoardRepository _boardRepository = new BoardRepository();
    

    [Theory]
    [ClassData(typeof(TestBoardMapsAndExpectedBoards))]
    public void Add_WorksAsExpected(string[,] testMap, Dictionary<string, Tile> testTiles)
    {
        //Arrange
        var testBoard = new Board(-1, testTiles, testMap.GetLength(0), testMap.GetLength(1));
        var expectedBoard = new Board(1, testBoard);

        //Act
        var id = _boardRepository.Add(testBoard);

        //Assert
        _boardRepository.Get(id).Should().BeEquivalentTo(expectedBoard);
    }
}