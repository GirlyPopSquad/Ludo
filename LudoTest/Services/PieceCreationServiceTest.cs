using FluentAssertions;
using LudoAPI.Models;
using LudoAPI.Models.Tiles;
using LudoAPI.Services;

namespace LudoTest.Services;

public class PieceCreationServiceTest
{
    [Fact]
    public void CreatePlayerPieces_Creates1ExpectedPiecePrHomeTile()
    {
        //Act
        var coordinates = new List<Coordinate>
        {
            new(0, 0),
            new(0, 1),
            new(1, 0),
            new(1, 1),
        };

        var colors = new List<Color>
        {
            Color.Blue,
            Color.Red,
            Color.Blue,
            Color.Red,
        };

        var homeTiles = coordinates
            .Select((t, i) => new HomeTile(t, colors[i]))
            .ToArray();

        var pieceNumber = 1;
        var expected = coordinates.Select((t, i) =>
        {
            var piece = new Piece(pieceNumber, colors[i], t);
            pieceNumber++;
            return piece;
        }).ToList();

        var pieceCreationService = new PieceCreationService();

        //Arrange
        var result = pieceCreationService.CreatePlayerPieces(homeTiles);

        //Assert
        result.Should().BeEquivalentTo(expected);
    }
}