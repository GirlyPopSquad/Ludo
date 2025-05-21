using LudoAPI.Models;
using LudoAPI.Repositories;

namespace LudoAPI.Services;

public class LobbyService : ILobbyService
{
    private readonly ILobbyRepository _lobbyRepo;

    public LobbyService(ILobbyRepository lobbyRepo)
    {
        _lobbyRepo = lobbyRepo;
    }

    public Lobby CreateLobby()
    {
        var lobbyPlayers = new List<Player>
        {
            new((Color)1),
            new((Color)2),
            new((Color)3),
            new((Color)4),
        };

        var lobby = _lobbyRepo.AddNewLobby(lobbyPlayers);

        return lobby;
    }

    public Lobby GetLobbyById(int id)
    {
        return _lobbyRepo.Get(id);
    }

    public void UpdateLobby(Lobby lobby)
    {
        _lobbyRepo.UpdateLobby(lobby);
    }

    public void Delete(int lobbyId)
    {
        _lobbyRepo.Remove(lobbyId);
    }
}