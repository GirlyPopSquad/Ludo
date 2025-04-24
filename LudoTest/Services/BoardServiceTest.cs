using FluentAssertions;
using LudoAPI.Models;
using LudoAPI.Services;

namespace LudoTest.Services;

public class BoardServiceTest
{
    
    
    [Fact]
    public void InitStandardBoard_CreatesAStandardBoardAsExpected()
    {
        //arrange
        BoardService service = new BoardService();
        var gameId = -1;
        
        //act
        var result = service.InitStandardBoard(gameId);

        //assert
        result.Should().BeOfType<Board>();
        result.GameId.Should().Be(gameId);
        result.Rows.Should().Be(15);
        result.Cols.Should().Be(15);
    }
}