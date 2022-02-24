using MinesweeperWithSolver.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

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
            var tiles = new List<Tile>();
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

        public void FirstMove(int x, int y, Random rand)
        {
            var depth = 0.125 * Width;
            var neighbors = GetNeighbors(x, y, (int)depth);
            neighbors.Add(GetTile(x, y));

            var mineList = Tiles
                        .Except(neighbors)
                        .OrderBy(t => rand.Next());
            var mineTiles = mineList
                        .Take(MineCount)
                        .ToList()
                        .Select(t => new
                        {
                            t.X_pos,
                            t.Y_pos
                        });

            foreach (var mineTile in mineTiles)
            {
                Tiles.Single(t => t.X_pos == mineTile.X_pos && t.Y_pos == mineTile.Y_pos)
                     .State = TileState.Mine;
            }

            foreach (var openTile in Tiles.Where(t => t.State == TileState.Blank))
            {
                var nearbyTiles = GetNeighbors(openTile.X_pos, openTile.Y_pos);
                openTile.AdjacentMines = nearbyTiles.Count(t => t.State == TileState.Mine);
            }
        }

        public Tile GetTile(int x, int y)
        {
            return Tiles.Where(t => t.X_pos == x && t.Y_pos == y).Single();
        }

        public List<Tile> GetNeighbors(int x, int y)
        {
            return GetNeighbors(x, y, 1);
        }

        public List<Tile> GetNeighbors(int x, int y, int depth)
        {
            var nearbyTiles = Tiles
                .Where(t => 
                        t.X_pos >= (x - depth) && t.X_pos <= (x + depth)
                        && t.Y_pos >= (y-depth) && t.Y_pos <= (y+depth)
                    );
            var currentTile = Tiles.Where(t => t.X_pos == x && t.Y_pos == y);
            return nearbyTiles.Except(currentTile).ToList();
        }
    }
}
