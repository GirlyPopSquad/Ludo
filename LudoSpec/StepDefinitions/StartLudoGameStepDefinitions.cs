using System;
using FluentAssertions;
using LudoAPI.Models;
using LudoAPI.Repositories;
using LudoAPI.Services;
using Reqnroll;

namespace LudoSpec.StepDefinitions
{
    [Binding]
    public class StartLudoGameStepDefinitions
    {

        private ILobbyService _lobbyService;
        private IStartingService _startingService;
        private Lobby _lobby = null;
        private IDiceService _diceService;
        private LobbyRepository _lobbyRepo;

        public StartLudoGameStepDefinitions()
        {

            _lobbyRepo = new LobbyRepository();
            _diceService = new DiceService();
            _lobbyService = new LobbyService(_lobbyRepo);
            _startingService = new StartingService(_diceService, _lobbyService);
        }


        [Given("a Lobby does not exist")]
        public void GivenALobbyDoesNotExist()
        {
            _lobby.Should().BeNull();
        }

        [Then("I want to create a new Lobby")]
        public void ThenIWantToCreateANewLobby()
        {
            _lobby = _lobbyService.CreateLobby();
            _lobby.Should().NotBeNull();
        }

        [Then("the starting rolls are initiated")]
        public void ThenTheStartingRollsAreInitiated()
        {
            _startingService.StartingRoll(_lobby);
        }

        [When("the first player rolls")]
        public void WhenTheFirstPlayerRolls()
        {
            _lobby.Rolls[0].Should().NotBeNull();   
        }

        [When("the second player rolls")]
        public void WhenTheSecondPlayerRolls()
        {
            _startingService.StartingRoll(_lobby);

            _lobby.Rolls[1].Should().NotBeNull();
        }

        [When("the third player rolls")]
        public void WhenTheThirdPlayerRolls()
        {
            _startingService.StartingRoll(_lobby);

            _lobby.Rolls[2].Should().NotBeNull();
        }

        [When("the fourth player rolls")]
        public void WhenTheFourthPlayerRolls()
        {
            _startingService.StartingRoll(_lobby);

            _lobby.Rolls[3].Should().NotBeNull();
        }

        [Then("all players have rolled the dice")]
        public void ThenAllPlayersHaveRolledTheDice()
        {
            _lobby.Rolls.Should().NotBeEmpty();
            _lobby.Rolls[0].Should().NotBeNull();
            _lobby.Rolls[1].Should().NotBeNull();
            _lobby.Rolls[2].Should().NotBeNull();
            _lobby.Rolls[3].Should().NotBeNull();
        }

        [Then("the game should start")]
        public void ThenTheGameShouldStart()
        {
            _lobby.Rolls.Should().NotBeEmpty();
            _lobby.Rolls[0].Should().NotBeNull();
            _lobby.Rolls[1].Should().NotBeNull();
            _lobby.Rolls[2].Should().NotBeNull();
            _lobby.Rolls[3].Should().NotBeNull();
        }
    }
}
