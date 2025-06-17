@LastRoundRule
Feature: Determine if its a players last Round
    
    Scenario: A player moves a piece to an Endtile, and we have to determine if its the last piece to move to End
        Given A gameId and a movablepiece, which have been chosen to move
        And A tile with the movablepieces potentialcoordinate exists on a board, with that gameId, and is an Endtile
        And There is a piece corresponding with the chosenpieces PieceNumber
        And The remaining of the players Endtiles, are occupied with pieces
        When That is accomplished
        Then The move will be the players last, otherwise its not