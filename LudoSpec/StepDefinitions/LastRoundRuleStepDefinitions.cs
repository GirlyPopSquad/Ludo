using FluentAssertions;
using LudoAPI.Models;
using LudoAPI.Models.Tiles;
using LudoAPI.Services;
using Moq;

namespace LudoSpec.StepDefinitions;

[Binding]
public class LastRoundRuleStepDefinitions
{
    private const int PieceNumber = 1;
    private const Color PlayerColor = Color.Red;
    private static readonly Coordinate EndtileCoordinate = new(1, 1);
    private static readonly EndTile EndTile = new(EndtileCoordinate, PlayerColor, new Move(0, -1));
    private int _gameId;
    private MovablePiece _movablePiece;

    private readonly Mock<IRollService> _rollServiceMock = new();
    private readonly Mock<IGameService> _gameServiceMock = new();
    private readonly Mock<IBoardService> _boardServiceMock = new();
    private readonly Mock<IPieceService> _pieceServiceMock = new();
    
    private bool _isLastMove;

    [Given("A gameId and a movablepiece, which have been chosen to move")]
    public void GivenAGameIdAndAMovablepieceWhichHaveBeenChosenToMove()
    {
        _gameId = 1;
        _movablePiece = new MovablePiece(PieceNumber, EndtileCoordinate);
    }

    [Given("A tile with the movablepieces potentialcoordinate exists on a board, with that gameId, and is an Endtile")]
    public void GivenTheTileWithTheMovablepiecesPotentialcoordinateOnABoardWithThatIdExistsAndIsAnEndtile()
    {
        _boardServiceMock.Setup(bs => bs.GetTileFromCoordinate(_gameId, EndtileCoordinate))
            .Returns(EndTile);
    }


    [Given("There is a piece corresponding with the chosenpieces PieceNumber")]
    public void GivenThereIsAPieceCorrespondingWithTheChosenpiecesPieceNumber()
    {
        Piece piece = new(PieceNumber, PlayerColor, new Coordinate(0, 1));

        _pieceServiceMock.Setup(ps => ps.GetPiece(_gameId, piece.PieceNumber))
            .Returns(piece);
    }

    [Given("The remaining of the players Endtiles, are occupied with pieces")]
    public void GivenTheRemainingOfThePlayersEndtilesAreOccupiedWithPieces()
    {
        EndTile[] endTiles = { EndTile };

        _boardServiceMock.Setup(bs => bs.GetEndTilesFromColor(_gameId, PlayerColor))
            .Returns(endTiles);


        List<Piece> remainingPieces =
        [
            new(2, PlayerColor, EndtileCoordinate),
            new(3, PlayerColor, EndtileCoordinate),
            new(4, PlayerColor, EndtileCoordinate)
        ];

        _pieceServiceMock.Setup(ps => ps.GetPieces(_gameId, (int)PlayerColor))
            .Returns(remainingPieces);
    }

    [When("That is accomplished")]
    public void WhenThatIsAccomplished()
    {
        RuleService ruleService = new RuleService(_rollServiceMock.Object, _pieceServiceMock.Object, _boardServiceMock.Object, _gameServiceMock.Object);

        _isLastMove = ruleService.WillThisBePlayersLastRound(_gameId, _movablePiece);
    }

    [Then("The move will be the players last, otherwise its not")]
    public void ThenTheMoveWillBeThePlayersLastOtherwiseItsNot()
    {
        _isLastMove.Should().Be(true);
    }
}