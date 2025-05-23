﻿using LudoAPI.Models;
using LudoAPI.Models.Tiles;
using LudoAPI.Repositories;
using LudoAPI.Services.Utils;

namespace LudoAPI.Services;

public class BoardService : IBoardService
{
    private readonly IBoardRepository _boardRepository;

    public BoardService(IBoardRepository boardRepository)
    {
        _boardRepository = boardRepository;
    }

    public Board GetBoard(int boardId)
    {
        return _boardRepository.Get(boardId);
    }

    public Board GetBoardFromGameId(int gameId)
    {
        return _boardRepository.GetByGameId(gameId);
    }

    public Tile GetTileFromCoordinate(int gameId, Coordinate coordinate)
    {
        var board = GetBoardFromGameId(gameId);
        var tiles = board.Tiles;

        return tiles[coordinate.ToString()];
    }

    public List<EndTile> GetEndTiles(int gameId)
    {
        return _boardRepository
            .GetByGameId(gameId).Tiles.Values
            .OfType<EndTile>()
            .ToList();
    }

    public HomeTile[] GetHomeTilesFromColor(int gameId, Color pieceColor)
    {
        return GetHomeTiles(gameId).Where(homeTile => homeTile.Color == pieceColor).ToArray();
    }

    public EndTile[] GetEndTilesFromColor(int gameId, Color pieceColor)
    {
        return _boardRepository.GetByGameId(gameId).Tiles.Values.OfType<EndTile>().Where(endTile => endTile.Color == pieceColor).ToArray();
    }

    public HomeTile[] GetHomeTiles(int gameId)
    {
        return _boardRepository.GetByGameId(gameId).Tiles.Values.OfType<HomeTile>().ToArray();
    }

    /// <summary>
    /// Initializes a standard Ludo board for a given game ID.
    /// </summary>
    /// <param name="gameId">The ID of the game for which the board is being initialized.</param>
    /// <returns>
    /// The ID of the newly created board.
    /// </returns>
    public int CreateStandardBoard(int gameId)
    {
        var boardMap = BoardMapLibrary.StandardBoard;
        var rows = boardMap.GetLength(0);
        var cols = boardMap.GetLength(1);

        var tiles = MakeTilesFromMap(boardMap);

        var newBoard = new Board(gameId, tiles, rows, cols);
        return _boardRepository.Add(newBoard);
    }

    public Dictionary<string, Tile> MakeTilesFromMap(string[,] boardMap)
    {
        var tiles = IdentifyTiles(boardMap);

        var startTiles = tiles.Values.OfType<StartTile>().ToList();
        var homeTiles = tiles.Values.OfType<HomeTile>().ToArray();

        //setup home tiles - happens here because StartTiles needs to be identified first
        foreach (var ht in homeTiles)
        {
            ht.StartTiles = startTiles.Where(tile => tile.Color == ht.Color).ToArray();
        }

        return tiles;
    }

    private Dictionary<string, Tile> IdentifyTiles(string[,] map)
    {
        Dictionary<string, Tile> tiles = new();
        //convert map to tiles
        for (int row = 0; row < map.GetLength(0); row++)
        {
            for (int col = 0; col < map.GetLength(1); col++)
            {
                var coordinate = new Coordinate(col, row);
                var tileString = map[row, col];

                var tile = BoardMapLibrary.TranslateToTile(tileString, coordinate);
                tiles.Add(coordinate.ToString(), tile);
            }
        }

        return tiles;
    }
}