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
        var lobbyPlayers = Enum.GetValues<Color>()
            .Select(color => new Player(color))
            .ToList();

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