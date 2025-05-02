using System;
using Reqnroll;

namespace LudoTest.StepDefinitions
{
    [Binding]
    public class StartNewGameStepDefinitions
    {
        [Given("that no player exists")]
        public void GivenThatNoPlayerExists()
        {
            throw new PendingStepException();
        }

        [When("I create a new game")]
        public void WhenICreateANewGame()
        {
            throw new PendingStepException();
        }

        [Then("the game should have no current player")]
        public void ThenTheGameShouldHaveNoCurrentPlayer()
        {
            throw new PendingStepException();
        }

        [Then("the game should have four players from the lobby")]
        public void ThenTheGameShouldHaveFourPlayersFromTheLobby()
        {
            throw new PendingStepException();
        }
    }
}
