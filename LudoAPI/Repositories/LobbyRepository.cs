using LudoAPI.Models;
using LudoAPI.Services;
using System.Reflection.Emit;

namespace LudoAPI.Repositories;

public class LobbyRepository : ILobbyRepository
{
    private readonly Dictionary<int, Lobby> _lobbies = new();
    private readonly IIdGeneratorService<Lobby> _idGenerator;

    public LobbyRepository(IIdGeneratorService<Lobby> idGenerator)
    {
        _idGenerator = idGenerator;
    }

    public Lobby AddNewLobby(List<Player> lobbyPlayers)
    {
        var lobbyId = _idGenerator.GetNewId(_lobbies);
        Lobby newLobby = new(lobbyId, lobbyPlayers);
        _lobbies.Add(lobbyId, newLobby);
        return newLobby;
    }

    public Lobby Get(int id)
    {
        return _lobbies[id];
    }

    public void UpdateLobby(Lobby lobby)
    {
        if (_lobbies.ContainsKey(lobby.Id))
        {
            _lobbies[lobby.Id] = lobby;
        }
        else
        {
            throw new KeyNotFoundException($"Game with ID {lobby.Id} not found.");
        }
    }

    public void Remove(int lobbyId)
    {
        _lobbies.Remove(lobbyId);
    }
}