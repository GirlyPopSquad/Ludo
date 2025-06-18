

Feature: Move Piece

  Scenario: Player moves a piece from home onto the board with a 6
    Given the player has a piece in the home area
    And the player rolls a 6
    When the player moves the piece
    Then the piece should be placed on the starting tile



