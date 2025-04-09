using LudoAPI.Models;

namespace LudoAPI.Repositories
{
    public class GameRepository : IGameRepository
    {
        private List<Game> Games { get; } = new List<Game>();

        public void Add(Game game)
        {
            throw new NotImplementedException();
        }

        public Game Get(int id)
        {
            throw new NotImplementedException();
        }

        public Game NewGame(int startingPlayerId, List<Player> players)
        {
            var newGame = new Game(GetNextId(), players, startingPlayerId);
            Games.Add(newGame);
            return newGame;
        }
        
        private int GetNextId()
        {
            if (Games.Count == 0) return 1;

            return Games.Last().Id + 1;
        }
    }
}
