Feature: Start New Game

  Scenario: Starting a new game with four players in the lobby
    Given that no player exists
    When I create a new game
    Then the game should have no current player
    And the game should have four players from the lobby
