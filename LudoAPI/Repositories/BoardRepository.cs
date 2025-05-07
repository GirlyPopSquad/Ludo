using LudoAPI.Models;

namespace LudoAPI.Repositories;

public class BoardRepository
{
    private Dictionary<int, Board> Boards = new ();

    public int Add(Board board)
    {
        var nextId = 1;

        if (Boards.Count >= 1)
        {
            nextId = Boards.Keys.Max() + 1;
        }
        
        var newBoard = new Board(nextId, board);
        Boards.Add(nextId, newBoard);
        
        return nextId;
    }

    public Board Get(int id)
    {
        return Boards[id];
    }
    
}