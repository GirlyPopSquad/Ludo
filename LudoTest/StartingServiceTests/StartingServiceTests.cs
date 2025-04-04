﻿using System.Collections;
using FluentAssertions;
using LudoAPI.Models;
using LudoAPI.Services;
using Moq;

namespace LudoTest.StartingServiceTests
{
    public class StartingServiceTests
    {
        private readonly Mock<IDiceService> _diceServiceMock;
        private readonly StartingService _startingService;
        private readonly Mock<ILobbyService> _lobbyServiceMock;

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
                    new Roll(new LobbyPlayer(1), 1),
                    new Roll(new LobbyPlayer(2), 2),
                    new Roll(new LobbyPlayer(3), 4),
                    new Roll(new LobbyPlayer(4), 4),
                },
                true
            ];

            yield return
            [
                new List<Roll>
                {
                    new Roll(new LobbyPlayer(1), 1),
                    new Roll(new LobbyPlayer(2), 2),
                    new Roll(new LobbyPlayer(3), 3),
                    new Roll(new LobbyPlayer(4), 4),
                },
                false
            ];

            yield return
            [
                new List<Roll>()
                {
                    new Roll(new LobbyPlayer(1), 1),
                    new Roll(new LobbyPlayer(2), 2),
                    new Roll(new LobbyPlayer(3), 2),
                    new Roll(new LobbyPlayer(4), 4),
                },
                false
            ];
        }

        [Theory]
        [ClassData(typeof(ReRollersData))]
        public void StartingService_GetRerollers_ShouldReturnTheRerollers(List<Roll> rolls, List<LobbyPlayer> expected)
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
                new LobbyPlayer(1),
                new LobbyPlayer(2),
                new LobbyPlayer(3),
                new LobbyPlayer(4)
            ]);

            Lobby expectedLobby = new Lobby(1, [
                new LobbyPlayer(1),
                new LobbyPlayer(2),
                new LobbyPlayer(3),
                new LobbyPlayer(4)
            ]);

            var expectedRoll = new Roll(new LobbyPlayer(1), 1);
            expectedLobby.Rolls.Add(expectedRoll);

            //Act
            var result = _startingService.StartingRoll(lobby);

            //Assert
            result.Should().BeEquivalentTo(expectedLobby);
        }

        [Fact]
        public void StartingService_HandleReroll()
        {
            //Arrange
            const int lobbyId = 1;
            var testplayer = new LobbyPlayer(1);
            var initialRoll = new Roll(testplayer, 6);

            var lobbyPlayers = new List<LobbyPlayer>
            {
                testplayer,
                new LobbyPlayer(2),
                new LobbyPlayer(3),
                new LobbyPlayer(4)
            };
            
            var initialLobby = new Lobby(lobbyId, lobbyPlayers);

            var initialRolls = new List<Roll>
            {
                initialRoll,
                new Roll(lobbyPlayers[1], 3),
                new Roll(lobbyPlayers[2], 6),
                new Roll(lobbyPlayers[3], 5),
            };
            
            initialLobby.Rolls = initialRolls;

            const int rerollValue = 4;
            var dicereroll = new Roll(testplayer, rerollValue);

            var updatedRolls = new List<Roll>
            {
                dicereroll,
                new Roll(lobbyPlayers[1], 3),
                new Roll(lobbyPlayers[2], 6),
                new Roll(lobbyPlayers[3], 5),
            };
            
            var updatedLobby = initialLobby;
            updatedLobby.Rolls = updatedRolls;
            
            _lobbyServiceMock.Setup(ls => ls.GetLobbyById(lobbyId)).Returns(initialLobby);
            _diceServiceMock.Setup(ds => ds.RollDice()).Returns(rerollValue);
            
            //Act
            var result = _startingService.HandleReroll(lobbyId, testplayer);

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
                    new Roll(new LobbyPlayer(1), 1),
                    new Roll(new LobbyPlayer(2), 2),
                    new Roll(new LobbyPlayer(3), 6),
                    new Roll(new LobbyPlayer(4), 6),
                },
                new List<LobbyPlayer>
                {
                    new LobbyPlayer(3),
                    new LobbyPlayer(4)
                }
            },
            new object[]
            {
                new List<Roll>
                {
                    new Roll(new LobbyPlayer(1), 1),
                    new Roll(new LobbyPlayer(2), 4),
                    new Roll(new LobbyPlayer(3), 4),
                    new Roll(new LobbyPlayer(4), 4),
                },
                new List<LobbyPlayer>()
                {
                    new LobbyPlayer(2),
                    new LobbyPlayer(3),
                    new LobbyPlayer(4)
                }
            }
        ];

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}