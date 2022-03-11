using MinesweeperWithSolver.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinesweeperWithSolver.Models
{
    public class GameBoard
    {
        public bool IsItFirstMove { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int MineCount { get; set; }
        public string PlayerName { get; set; }
        public List<Tile> Tiles { get; set; }
        public GameStatus Status { get; set; }
        public DateTime GameStartTime { get; set; }
        public DateTime GameEndTime { get; set; }

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
            GameStartTime = DateTime.Now;
            Status = GameStatus.Idle;
            IsItFirstMove = true;
        }

        public void InitializeGameBoard()
        {
            Tiles = CreateTiles(Width, Height);
            GameStartTime = DateTime.Now;
            Status = GameStatus.Idle;
            IsItFirstMove = true;
        }

        public List<Tile> CreateTiles(int width, int height)
        {
            var tiles = new List<Tile>();
            int tileId = 0;
            for(int x = 0; x< height; x++)
            {
                for(int y = 0; y< width; y++)
                {
                    tiles.Add(new Tile(tileId, x, y));
                    tileId++;
                }
            }
            return tiles;
        }
        public Tile GetTile(int x, int y)
        {
            return Tiles.Where(t => t.X_pos == x && t.Y_pos == y).Single();
        }
        public List<Tile> GetNeighbors(Tile tile)
        {
            return GetNeighbors(tile.X_pos, tile.Y_pos, 1);
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
                        && t.Y_pos >= (y - depth) && t.Y_pos <= (y + depth)
                    );
            var currentTile = Tiles.Where(t => t.X_pos == x && t.Y_pos == y);
            return nearbyTiles.Except(currentTile).ToList();
        }

        public void SetImage(Tile tile)
        {
            switch (tile.State)
            {
                case TileState.Mine:
                    tile.Image = "/Resources/Images/mine.png";
                    break;
                case TileState.Blank:
                    tile.Image = "/Resources/Images/covered.png";
                    break;
                default:
                    tile.Image = @"/Resources/Images/" + tile.AdjacentMines + ".png";
                    break;
            }
        }

        public void FirstMove(int x, int y)
        {
            Random rand = new Random();
            IsItFirstMove = false;
            Status = GameStatus.InProgress;

            var depth = 0.125 * Width;
            var neighbors = GetNeighbors(x, y, (int)depth);
            neighbors.Add(GetTile(x, y));

            var mineList = Tiles
                        .Except(neighbors)
                        .OrderBy(t => rand.Next());

            var mineTiles = mineList
                        .Take(MineCount)
                        .ToList()
                        .Select(t => new { t.X_pos, t.Y_pos });

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

        public void RevealZeros(int x, int y)
        {
            var neighbors = GetNeighbors(x, y)
                            .Where(t => t.State == TileState.Blank);

            foreach (var neightbor in neighbors)
            {
                neightbor.State = TileState.Revealed;
                SetImage(neightbor);

                if ( neightbor.AdjacentMines == 0)
                {
                    RevealZeros(neightbor.X_pos, neightbor.Y_pos);
                }
            }
        }

        public void RevealTile(int x, int y)
        {
            var selected = Tiles.First(t => t.X_pos == x && t.Y_pos == y);

            if(!selected.IsFlagged)
            {
                if(selected.State == TileState.Mine)
                {
                    GameEndTime = DateTime.Now;
                    Status = GameStatus.Failed;
                    RevealAllMines(selected);
                }
                else
                {
                    selected.State = TileState.Revealed;
                    SetImage(selected);
                }


                if ((selected.State != TileState.Mine && !selected.IsFlagged) && selected.AdjacentMines == 0)
                {
                    RevealZeros(x, y);
                }

                if(selected.State != TileState.Mine)
                {
                    CompletionCheck();
                }
            }
        }

        public void FlagTile(int x, int y)
        {
            var selected = Tiles.First(t => t.X_pos == x && t.Y_pos == y);

            if(selected.State != TileState.Revealed)
            {
                if (selected.IsFlagged)
                {
                    selected.IsFlagged = false;
                    selected.Image = @"/Resources/Images/covered.png";

                }
                else
                {
                    selected.IsFlagged = true;
                    selected.Image = @"/Resources/Images/flag.png";
                }
            }
        }

        public void RevealAllMines(Tile selected)
        {
            foreach (var tile in Tiles.Where(t => t.State == TileState.Mine))
            {
                SetImage(tile);
            }
            selected.Image = @"/Resources/Images/redmine.png";
        }

        public void CompletionCheck()
        {
            var nonMineTiles = Tiles
                                .Where(t => !(t.State == TileState.Mine))
                                .Select(t => t.tileID)
                                .ToArray();

            var revealedNonMineTiles = Tiles
                                .Where(t => t.State == TileState.Revealed)
                                .Select(t => t.tileID)
                                .ToArray();

            if(nonMineTiles.Length == revealedNonMineTiles.Length)
            {
                Status = GameStatus.Finished;
            }
        }
    }
}
