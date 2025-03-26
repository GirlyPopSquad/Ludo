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


        [Theory]
        [InlineData(6, true)]
        [InlineData(5, false)]
        public void DiceService_IsItA6_ShouldReturnExpectedResult(int input, bool expected)
        {
            // Arrange
            DiceService service = new DiceService();

            // Act
            bool result = service.IsItA6(input);

            // Assert
            result.Should().Be(expected);
        }

    }
}
