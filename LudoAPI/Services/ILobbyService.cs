using LudoAPI.Models;

namespace LudoAPI.Services;

public interface ILobbyService
{
    Lobby CreateLobby();
    Lobby GetLobbyById(int id);
    void UpdateLobby(Lobby lobby);
}