using LudoAPI.Models;
using LudoAPI.Repositories;

namespace LudoAPI.Services;

public class LobbyService : ILobbyService
{

    private readonly LobbyRepository _lobbyRepo;

    public LobbyService(LobbyRepository lobbyRepo)
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
}