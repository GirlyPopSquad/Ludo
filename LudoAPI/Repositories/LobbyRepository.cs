using LudoAPI.Models;

namespace LudoAPI.Repositories
{
    public class LobbyRepository : ILobbyRepository
    {
        public List<Lobby> Lobbies { get; } = new List<Lobby>();

        public Lobby AddNewLobby(List<Player> lobbyPlayers)
        {
            Lobby newLobby = new(GetNextId(), lobbyPlayers);
            Lobbies.Add(newLobby);
            return newLobby;
        }

        private int GetNextId()
        {
            if (Lobbies.Count == 0)
            {
                return 1;
            }

            return Lobbies[Lobbies.Count - 1].Id + 1;
        }

        public void RemoveOldRolls(int lobbyId, List<Player> rerollers)
        {
            var lobby = Lobbies.First(lobby => lobby.Id == lobbyId);
            var rollsToKeep = new List<Roll>(); 
            foreach(var roll in lobby.Rolls)
            {
                foreach(var reroller in rerollers)
                {
                    if (roll.PlayerId == reroller.Id)
                    {
                        rollsToKeep.Add(roll);
                    }
                }   
            }
            lobby.Rolls = rollsToKeep;
        }

        public Lobby Get(int id)
        {
            return Lobbies.First(lobby => lobby.Id == id);
        }

        public void UpdateLobby(Lobby lobby)
        {
            var existingLobby = Lobbies.FirstOrDefault(l => l.Id == lobby.Id);
            if (existingLobby != null)
            {
                existingLobby.Players = lobby.Players;
                existingLobby.Rolls = lobby.Rolls;
            }
            else
            {
                throw new Exception("Lobby not found");
            }
        }
    }
}