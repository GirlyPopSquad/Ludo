using FluentAssertions;
using LudoAPI.Models;

namespace LudoTest;

public class FluentExamples
{
    private readonly Player _player = new(Color.Blue);
    
    [Fact]
    public void Standard_UnitTest()
    {
        Assert.Equal(Color.Blue, _player.Color);
    }

    [Fact]
    public void Fluent_UnitTest()
    {
        _player.Color.Should().Be(Color.Blue);
    }
}