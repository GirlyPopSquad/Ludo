using FluentAssertions;
using LudoAPI.Models;
using LudoAPI.Models.Tiles;
using LudoAPI.Services;

namespace LudoTest.Services;

public class PieceCreationServiceTest
{
    [Fact]
    public void CreatePlayerPieces_CreatesExpectedPiecesFromHomeTiles_OneOfEachColor()
    {
        //Arrange
        var homeCoordinates = new List<Coordinate>
            { new(0, 0), new(0, 1), new(1, 0), new(1, 1) };

        var colors = new List<Color>
            { Color.Blue, Color.Red, Color.Blue, Color.Red };

        var homeTiles = homeCoordinates
            .Select((t, i) => new HomeTile(t, colors[i]))
            .ToArray();

        var pieceNumber = 1;
        var expected = homeCoordinates
            .Select((t, i) =>
            {
                var piece = new Piece(pieceNumber, colors[i], t);
                pieceNumber++;
                return piece;
            }).ToList();

        var pieceCreationService = new PieceCreationService();

        //Act
        var result = pieceCreationService
            .CreatePlayerPieces(homeTiles);

        //Assert
        result.Should().BeEquivalentTo(expected);
    }
    
    [Theory]
    [MemberData(nameof(GetHomeTilesAndCorrespondingPieces))]
    public void CreatePlayerPieces_CreatesExpectedPiecesFromHomeTiles(HomeTile[] homeTiles, Piece[] expected)
    {
        //Arrange
        var pieceCreationService = new PieceCreationService();

        //Act
        var result = pieceCreationService
            .CreatePlayerPieces(homeTiles);

        //Assert
        result.Should().BeEquivalentTo(expected);
    }
    

    public static IEnumerable<object[]> GetHomeTilesAndCorrespondingPieces()
    {
        yield return
        [
            new HomeTile[]
            {
                new(new Coordinate(2, 2), Color.Red),
                new(new Coordinate(2, 3), Color.Red),
                new(new Coordinate(3, 2), Color.Red),
                new(new Coordinate(3, 3), Color.Red),
                new(new Coordinate(11, 2), Color.Green),
                new(new Coordinate(12, 2), Color.Green),
                new(new Coordinate(11, 3), Color.Green),
                new(new Coordinate(12, 3), Color.Green),
                new(new Coordinate(2, 11), Color.Blue),
                new(new Coordinate(3, 11), Color.Blue),
                new(new Coordinate(2, 12), Color.Blue),
                new(new Coordinate(3, 12), Color.Blue),
                new(new Coordinate(11, 11), Color.Yellow),
                new(new Coordinate(12, 11), Color.Yellow),
                new(new Coordinate(11, 12), Color.Yellow),
                new(new Coordinate(12, 12), Color.Yellow),
            },
            new Piece[]
            {
                new(1, Color.Red, new Coordinate(2, 2)),
                new(2, Color.Red, new Coordinate(2, 3)),
                new(3, Color.Red, new Coordinate(3, 2)),
                new(4, Color.Red, new Coordinate(3, 3)),
                new(5, Color.Green, new Coordinate(11, 2)),
                new(6, Color.Green, new Coordinate(12, 2)),
                new(7, Color.Green, new Coordinate(11, 3)),
                new(8, Color.Green, new Coordinate(12, 3)),
                new(9, Color.Blue, new Coordinate(2, 11)),
                new(10, Color.Blue, new Coordinate(3, 11)),
                new(11, Color.Blue, new Coordinate(2, 12)),
                new(12, Color.Blue, new Coordinate(3, 12)),
                new(13, Color.Yellow, new Coordinate(11, 11)),
                new(14, Color.Yellow, new Coordinate(12, 11)),
                new(15, Color.Yellow, new Coordinate(11, 12)),
                new(16, Color.Yellow, new Coordinate(12, 12)),
            }
        ];
    }
}