using LudoAPI.Models;
using LudoAPI.Repositories;

namespace LudoAPI.Services;

public class LobbyService : ILobbyService
{
    private readonly ILobbyRepository _lobbyRepo;
    private readonly IPlayerGenerator _playerGenerator;
    private readonly IIdGeneratorService<Lobby> _idGenerator;

    public LobbyService(ILobbyRepository lobbyRepo, 
                        IPlayerGenerator playerGenerator, 
                        IIdGeneratorService<Lobby> idGenerator)
    {
        _idGenerator = idGenerator;
        _lobbyRepo = lobbyRepo;
        _playerGenerator = playerGenerator;
    }

    public Lobby CreateLobby()
    {
        var players = _playerGenerator.GeneratePlayers();
        var lobbyId = _idGenerator.GetNewId(_lobbyRepo.GetLobbies());
        var lobby = new Lobby(lobbyId, players);
        _lobbyRepo.Save(lobby);   

        return lobby;
    }

    public Lobby GetLobbyById(int id)
    {
        return _lobbyRepo.Get(id);
    }

    public void UpdateLobby(Lobby lobby)
    {
        _lobbyRepo.Update(lobby);
    }

    public void Delete(int lobbyId)
    {
        _lobbyRepo.Remove(lobbyId);
    }
}
