using LudoAPI.Models;

namespace LudoAPI.Repositories
{
    public class GameRepository : IGameRepository
    {
        private Dictionary<int, Game> Games { get; } = new();

        public int Add(Game game)
        {

            var nextId = 1;
            
            if (Games.Count >= 1)
            {
                nextId = Games.Keys.Max() + 1;
            }
            
            var newGame = new Game(nextId, game);
            Games.Add(nextId, newGame);
            
            return nextId;
        }

        public Game Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}