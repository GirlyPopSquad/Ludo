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
        var lobbyPlayers = new List<LobbyPlayer>()
        {
            new LobbyPlayer(1),
            new LobbyPlayer(2),
            new LobbyPlayer(3),
            new LobbyPlayer(4),
        };

        Lobby lobby = _lobbyRepo.AddNewLobby(lobbyPlayers);

        return lobby;
    }

    public Lobby GetLobbyById(int id)
    {
        return _lobbyRepo.Get(id);
    }
}