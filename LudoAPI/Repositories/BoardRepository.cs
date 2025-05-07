using LudoAPI.Models;

namespace LudoAPI.Repositories;

public class BoardRepository : IBoardRepository
{
    private readonly Dictionary<int, Board> _boards = new ();

    public int Add(Board board)
    {
        var nextId = 1;

        if (_boards.Count >= 1)
        {
            nextId = _boards.Keys.Max() + 1;
        }
        
        var newBoard = new Board(nextId, board);
        _boards.Add(nextId, newBoard);
        
        return nextId;
    }

    public Board Get(int id)
    {
        return _boards[id];
    }

    public Board GetByGameId(int gameId)
    {
        return _boards.Values.First(b=> b.GameId == gameId);
    }
    
}