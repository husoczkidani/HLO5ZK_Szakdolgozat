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
        public string PlayerName { get; set; }
        public List<Tile> Tiles { get; set; }
        public GameStatus Status { get; set; }
        public DateTime GameStartTime { get; set; }
        public DateTime GameEndTime { get; set; }

        public GameBoard()
        {
        }

        public void InitializeGameBoard(int difficulty, string playerName)
        {

            
            switch (difficulty)
            {
                case 1:
                    Width = 9;
                    Height = 9;
                    MineCount = 10;
                    break;
                case 2:
                    Width = 16;
                    Height = 16;
                    MineCount = 40;
                    break;
                case 3:
                    Width = 30;
                    Height = 16;
                    MineCount = 99;
                    break;
            }
            PlayerName = playerName;
            Tiles = CreateTiles(Width, Height);
            Status = GameStatus.InProgress;
            GameStartTime = DateTime.Now;
        }

        public List<Tile> CreateTiles(int width, int height)
        {
            List<Tile> tiles = new List<Tile>();
            int tileId = 0;
            for(int x = 0; x< width; x++)
            {
                for(int y = 0; y< height; y++)
                {
                    tiles.Add(new Tile(tileId, x, y));
                    tileId++;
                }
            }
            return tiles;
        }
    }
}
