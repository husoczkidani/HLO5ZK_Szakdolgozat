using MinesweeperWithSolver.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinesweeperWithSolver.Models
{
    public class GameBoard
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int MineCount { get; set; }
        public List<Tile> Tiles { get; set; }
        public GameStatus Status { get; set; }

        public GameBoard(int size, int mines)
        {
            Width = size;
            Height = size;
            MineCount = mines;
            Tiles = CreateTiles(size);
            Status = GameStatus.InProgress;
        }

        public List<Tile> CreateTiles(int boardSize)
        {
            List<Tile> tiles = new List<Tile>();
            int tileId = 0;
            for(int x = 0; x<boardSize; x++)
            {
                for(int y = 0; y<boardSize; y++)
                {
                    tiles.Add(new Tile(tileId, x, y));
                }
            }
            return tiles;
        }
    }
}
