using LudoAPI.Models;

namespace LudoAPI.Repositories;

public interface ILobbyRepository
{
    void Save(Lobby lobby);
    Lobby Get(int id);
    void Update(Lobby lobby);
    void Remove(int lobbyId);
    Dictionary<int, Lobby> GetLobbies();
}