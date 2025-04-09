using System.Collections;
using LudoAPI.Models;

namespace LudoTest.GameServiceTests;

public class LobbyTestData : IEnumerable<object[]>
{
    private readonly List<object[]> _data =
    [
        new object[]
        {
            new Lobby(1, [
                new LobbyPlayer(1),
                new LobbyPlayer(2),
                new LobbyPlayer(3),
                new LobbyPlayer(4)
            ])
        }
    ];

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}