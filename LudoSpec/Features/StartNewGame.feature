@StartNewGame
Feature: Start Ludo Game
  In order to play Ludo
  As a user
  I want to start a new game

  @StartGame
  Scenario: Starting a new Ludo game
    Given a Lobby does not exist
    Then I want to create a new Lobby
    And the starting rolls are initiated
    When the first player rolls
    And the second player rolls
    And the third player rolls
    And the fourth player rolls
    Then all players have rolled the dice
    And the game should start