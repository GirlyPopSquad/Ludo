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


        [Fact]
        public void DiceService_IsItA6_TrueIf6()
        {
            //Arrage
            DiceService service = new DiceService();
            

            //Act
            bool result = service.IsItA6(6);
            //service.RollDice();

            //Assert
            result.Should().Be(true);
        }

        [Fact]
        public void DiceService_IsItA6_ReturnsFalseIfNot6()
        {
            //Arrage
            DiceService service = new DiceService();


            //Act
            bool result = service.IsItA6(5);
            //service.RollDice();

            //Assert
            result.Should().Be(false);
        }

    }
}
