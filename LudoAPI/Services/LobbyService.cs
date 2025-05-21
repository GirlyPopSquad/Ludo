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
        var lobbyPlayers = new List<Player>()
        {
            new Player(1),
            new Player(2),
            new Player(3),
            new Player(4),
        };

        Lobby lobby = _lobbyRepo.AddNewLobby(lobbyPlayers);

        return lobby;
    }

    public Lobby GetLobbyById(int id)
    {
        return _lobbyRepo.Get(id);
    }

    public void RemoveOldRolls(int id, List<Player> rerollers)
    {
        _lobbyRepo.RemoveOldRolls(id, rerollers);
    }

    public void UpdateLobby(Lobby lobby)
    {
        _lobbyRepo.UpdateLobby(lobby);
    }
}