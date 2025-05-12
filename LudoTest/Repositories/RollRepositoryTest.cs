using FluentAssertions;
using LudoAPI.Repositories;

namespace LudoTest.Repositories;

public class RollRepositoryTest
{
    private RollRepository _rollRepository = new RollRepository();
    
    [Fact]
    public void GetRollsFromGame_ReturnsNullWhenNoRollsExists()
    {
        //arrange
        
        //act
        var result = _rollRepository.GetRollsFromGame(-1);

        //assert
        result.Should().BeNull();
    }
}