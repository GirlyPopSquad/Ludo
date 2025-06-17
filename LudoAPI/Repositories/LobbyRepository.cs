using LudoAPI.Models;
using LudoAPI.Services;
using System.Reflection.Emit;

namespace LudoAPI.Repositories;

public class LobbyRepository : ILobbyRepository
{
    private readonly Dictionary<int, Lobby> _lobbies = new();

    public void Save(Lobby lobby)
    {
        _lobbies[lobby.Id] = lobby;
    }

    public Lobby Get(int id)
    {
        return _lobbies[id];
    }

    public void Update(Lobby lobby)
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

    public Dictionary<int, Lobby> GetLobbies()
    {
       return _lobbies;
    }
}
