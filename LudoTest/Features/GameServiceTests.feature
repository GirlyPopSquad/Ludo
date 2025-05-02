   Feature: Start New Game
     As a game service
     I want to start a new game
     So that players from the lobby can participate

     Scenario: Starting a new game with no current player
       Given That no player exists
       When I start a new game
       Then The game should have no current player and four players from the lobby
   