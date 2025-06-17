using LudoAPI.Models;
using LudoAPI.Repositories;

namespace LudoAPI.Services;

public class LobbyService : ILobbyService
{
    private readonly ILobbyRepository _lobbyRepo;
    private readonly IPlayerGenerator _playerGenerator;

    public LobbyService(ILobbyRepository lobbyRepo, IPlayerGenerator playerGenerator)
    {
        _lobbyRepo = lobbyRepo;
        _playerGenerator = playerGenerator;
    }

    public Lobby CreateLobby()
    {
        var players = _playerGenerator.GeneratePlayers();
        var lobby = _lobbyRepo.AddNewLobby(players);

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
