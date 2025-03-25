using LudoAPI.Models;

namespace LudoAPI.Repositories
{
    public class LobbyRepository
    {
        public List<Lobby> Lobbies { get; } = new List<Lobby>();

        public Lobby AddNewLobby(List<LobbyPlayer> lobbyPlayers)
        {
            Lobby newLobby = new(lobbyPlayers, GetNextId());
            Lobbies.Add(newLobby);
            return newLobby;
        }

        private int GetNextId()
        {
            if (Lobbies.Count() == 0)
            {
                return 1;
            }
            else
            {
               return Lobbies[Lobbies.Count()-1].Id+1;
            }
        }

        public Lobby Get(int id)
        {
            return Lobbies.First(lobby => lobby.Id == id);
        }
    }
}
