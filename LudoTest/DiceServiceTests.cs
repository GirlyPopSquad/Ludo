using FluentAssertions;
using LudoAPI.Services;

namespace LudoTest
{
    public class DiceServiceTests
    {
        [Fact]
        public void DiceService_RollDice_ShouldReturnNumberBetween1And6()
        {
            //Arrange
            DiceService service = new DiceService();
            int[] acceptableNumbers = {1, 2, 3, 4, 5, 6};

            //Act
            var result = service.RollDice();

            //Assert
            result.Should().BeOneOf(acceptableNumbers);
        }
    }
}
