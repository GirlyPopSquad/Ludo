using LudoAPI.Models;

namespace LudoAPI.Repositories
{
    public class LobbyRepository : ILobbyRepository
    {
        //todo: switch to dictionary
        private readonly Dictionary<int, Lobby> _lobbies = new();

        public Lobby AddNewLobby(List<Player> lobbyPlayers)
        {
            var lobbyId = GetNextId();
            Lobby newLobby = new(lobbyId, lobbyPlayers);
            _lobbies.Add(lobbyId, newLobby);
            return newLobby;
        }

        private int GetNextId()
        {
            if (_lobbies.Count == 0)
            {
                return 1;
            }

            return _lobbies.Keys.Max() + 1;
        }

        public Lobby Get(int id)
        {
            return _lobbies[id];
        }

        public void UpdateLobby(Lobby lobby)
        {
            var lobbyId = lobby.Id;

            if (_lobbies.ContainsKey(lobbyId))
            {
                _lobbies[lobbyId] = lobby;
            }
            else
            {
                throw new KeyNotFoundException($"Game with ID {lobbyId} not found.");
            }
        }
    }
}