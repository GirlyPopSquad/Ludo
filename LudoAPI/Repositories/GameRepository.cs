using LudoAPI.Models;

namespace LudoAPI.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly Dictionary<int, Game> _games = new();

        public int Add(Game game)
        {
            var nextId = 1;
            
            if (_games.Count >= 1)
            {
                nextId = _games.Keys.Max() + 1;
            }
            
            var newGame = new Game(nextId, game);
            _games.Add(nextId, newGame);
            
            return nextId;
        }

        public Game Get(int id)
        {
            return _games[id];
        }

        public void Update(int id, Game game)
        {
            if (_games.ContainsKey(id))
            {
                _games[id] = game;
            }
            else
            {
                throw new KeyNotFoundException($"Game with ID {id} not found.");
            }
        }
    }
}