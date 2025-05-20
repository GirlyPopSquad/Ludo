using System.Collections;
using FluentAssertions;
using LudoAPI.Models;
using LudoAPI.Services;
using LudoTest.Shared;
using Moq;

namespace LudoTest.StartingServiceTests;

public class StartingServiceTests
{
    private readonly Mock<IDiceService> _diceServiceMock;
    private readonly StartingService _startingService;
    private readonly Mock<ILobbyService> _lobbyServiceMock;
        
    private static readonly Player BluePlayer = PlayerTestData.BluePlayer;
    private static readonly Player RedPlayer = PlayerTestData.RedPlayer;
    private static readonly Player GreenPlayer = PlayerTestData.GreenPlayer;
    private static readonly Player YellowPlayer = PlayerTestData.YellowPlayer;

    public StartingServiceTests()
    {
        _diceServiceMock = new Mock<IDiceService>();
        _lobbyServiceMock = new Mock<ILobbyService>();
        _startingService = new StartingService(_diceServiceMock.Object, _lobbyServiceMock.Object);
    }

    [Theory]
    [MemberData(nameof(GetRollsDataAllPlayersRolledOnce))]
    public void StartingService_ShouldReRoll_ReturnsExpectedResult(List<Roll> rolls, bool expected)
    {
        // Act
        var shouldTheyReroll = _startingService.ShouldReRoll(rolls);

        // Assert
        shouldTheyReroll.Should().Be(expected);
    }

    public static IEnumerable<object[]> GetRollsDataAllPlayersRolledOnce()
    {
        yield return
        [
            new List<Roll>
            {
                new(1, 1),
                new(2, 2),
                new(3, 4),
                new(4, 4),
            },
            true
        ];

        yield return
        [
            new List<Roll>
            {
                new(1, 1),
                new(2, 2),
                new(3, 3),
                new(4, 4),
            },
            false
        ];

        yield return
        [
            new List<Roll>()
            {
                new(1, 1),
                new(2, 2),
                new(3, 2),
                new(4, 4),
            },
            false
        ];
    }

    [Theory]
    [ClassData(typeof(ReRollersData))]
    public void StartingService_GetRerollers_ShouldReturnTheRerollers(List<Roll> rolls, List<Player> expected)
    {
        //Act
        var actual = _startingService.GetReRollers(rolls);

        //Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void StartingRollTest_StartingRoll_ShouldReturnOneStartingRoll()
    {
        //Arrange
        _diceServiceMock.Setup(service => service.RollDice()).Returns(1);

        Lobby lobby = new Lobby(1, [
            BluePlayer,
            RedPlayer,
            GreenPlayer,
            YellowPlayer
        ]);

        Lobby expectedLobby = new Lobby(1, [
            BluePlayer,
            RedPlayer,
            GreenPlayer,
            YellowPlayer
        ]);

        var expectedRoll = new Roll(BluePlayer.Id, 1);
        expectedLobby.Rolls.Add(expectedRoll);

        //Act
        var result = _startingService.DoNextStartingRoll(lobby);

        //Assert
        result.Should().BeEquivalentTo(expectedLobby);
    }

    [Fact]
    public void StartingService_HandleReroll()
    {
        //Arrange
        const int lobbyId = 1;
        var testplayer = BluePlayer;
        var initialRoll = new Roll(testplayer.Id, 6);

        var lobbyPlayers = new List<Player>
        {
            testplayer,
            RedPlayer,
            GreenPlayer,
            YellowPlayer
        };
            
        var initialLobby = new Lobby(lobbyId, lobbyPlayers);

        var initialRolls = new List<Roll>
        {
            initialRoll,
            new(lobbyPlayers[1].Id, 3),
            new(lobbyPlayers[2].Id, 6),
            new(lobbyPlayers[3].Id, 5),
        };
            
        initialLobby.Rolls = initialRolls;

        const int rerollValue = 4;
        var dicereroll = new Roll(testplayer, rerollValue);

        var updatedRolls = new List<Roll>
        {
            dicereroll,
            new(lobbyPlayers[1].Id, 3),
            new(lobbyPlayers[2].Id, 6),
            new(lobbyPlayers[3].Id, 5),
        };
            
        var updatedLobby = initialLobby;
        updatedLobby.Rolls = updatedRolls;
            
        _lobbyServiceMock.Setup(ls => ls.GetLobbyById(lobbyId)).Returns(initialLobby);
        _diceServiceMock.Setup(ds => ds.RollDice()).Returns(rerollValue);
            
        //Act
        var result = _startingService.HandleReroll(lobbyId, testplayer.Id);

        //Assert
        _lobbyServiceMock.Verify(ls => ls.GetLobbyById(lobbyId), Times.Once);
        _lobbyServiceMock.Verify(ls => ls.UpdateLobby(It.IsAny<Lobby>()), Times.Once);
            
        result.Should().BeEquivalentTo(updatedLobby);
    }
}

public class ReRollersData : IEnumerable<object[]>
{
    private readonly List<object[]> _data =
    [
        new object[]
        {
            new List<Roll>
            {
                new(1, 1),
                new(2, 2),
                new(3, 6),
                new(4, 6),
            },
            new List<Player>
            {
                new(Color.Yellow),
                new(Color.Blue)
            }
        },
        new object[]
        {
            new List<Roll>
            {
                new(1, 1),
                new(2, 4),
                new(3, 4),
                new(4, 4),
            },
            new List<Player>
            {
                new(Color.Green),
                new(Color.Yellow),
                new(Color.Blue)
            }
        }
    ];

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}