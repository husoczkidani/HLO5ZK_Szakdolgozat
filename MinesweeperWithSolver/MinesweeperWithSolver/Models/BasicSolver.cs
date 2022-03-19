using System.Linq;
using System.Data;
using MinesweeperWithSolver.Enums;
using System.Collections.Generic;
using System;

namespace MinesweeperWithSolver.Models
{
    public class BasicSolver
    {
        public int GamesPlayed { get; set; }
        public int GamesSolved { get; set; }
        public int GamesFailed { get; set; }
        public double MinesFlagged { get; set; }
        public double TilesRevealed { get; set; }

        private readonly GameBoard _gameBoard;

        public BasicSolver(GameBoard gameBoard)
        {
            _gameBoard = gameBoard;
        }

        public void Solver()
        {
            while(_gameBoard.Status == GameStatus.InProgress)
            {
                if(!SearchForObviousMines() && !SearchForObviousNumbers())
                {
                    GuessRandomNeighboringTile();
                }
            }
            
        }

        public void Solver(int difficulty, int numberOfSimulations)
        {
            GamesSolved = 0; 
            GamesFailed = 0;
            MinesFlagged = 0;
            TilesRevealed = 0;

            double minesFlagged = 0;
            double tilesRevealed = 0;

            _gameBoard.InitializeGameBoard(difficulty);
            for (GamesPlayed = 0; GamesPlayed < numberOfSimulations; GamesPlayed++)
            {
                _gameBoard.FirstMove(0, 0);
                _gameBoard.RevealTile(0, 0);
                Solver();
                if (_gameBoard.Status == GameStatus.Finished) GamesSolved++;
                if (_gameBoard.Status == GameStatus.Failed) GamesFailed++;

                minesFlagged += _gameBoard.Tiles.Where(t => t.IsFlagged == true).Count();
                tilesRevealed += _gameBoard.Tiles.Where(t => t.State == TileState.Revealed).Count();

                _gameBoard.InitializeGameBoard();
            }

            MinesFlagged = minesFlagged / (double)(GamesPlayed * _gameBoard.MineCount)*100;
            TilesRevealed = tilesRevealed / (double)(GamesPlayed * _gameBoard.Tiles.Count())*100;
        }

        public bool SearchForObviousMines()
        {
            bool foundMines = false;

            var tilesWithNeighbors = GetTilesWithBlankNeighbors();
            foreach(var tile in tilesWithNeighbors)
            {
                var blankNeighbors = _gameBoard.GetNeighbors(tile).Where(n => n.State != TileState.Revealed && !n.IsFlagged);
                var flaggedNeighbors = _gameBoard.GetNeighbors(tile).Where(n => n.IsFlagged);

                if(tile.AdjacentMines - flaggedNeighbors.Count() == blankNeighbors.Count())
                {
                    foundMines = true;
                    foreach (var t in blankNeighbors)
                    {
                        _gameBoard.FlagTile(t.X_pos,t.Y_pos);
                    }
                }
            }

            return foundMines;
        }

        public bool SearchForObviousNumbers()
        {
            bool foundNumbers = false;

            var tilesWithNeighbors = GetTilesWithBlankNeighbors();
            foreach (var tile in tilesWithNeighbors)
            {
                var blankNeighbors = _gameBoard.GetNeighbors(tile).Where(n => n.State != TileState.Revealed && !n.IsFlagged);
                var flaggedNeighbors = _gameBoard.GetNeighbors(tile).Where(n => n.IsFlagged);

                if (tile.AdjacentMines == flaggedNeighbors.Count())
                {
                    foundNumbers = true;
                    foreach (var t in blankNeighbors)
                    {
                        _gameBoard.RevealTile(t.X_pos, t.Y_pos);
                    }
                }
            }

            return foundNumbers;
        }

        public void GuessRandomNeighboringTile()
        {
            var blankTilesWithNeighbors = GetBlankTilesWithNeighbors();
            Random rand = new Random();
            int guess = rand.Next(0, blankTilesWithNeighbors.Count() - 1);
            _gameBoard.RevealTile(blankTilesWithNeighbors[guess].X_pos, blankTilesWithNeighbors[guess].Y_pos);

        }

        public IEnumerable<Tile> GetTilesWithBlankNeighbors()
        {
            return _gameBoard.Tiles
                    .Where(t => t.State == TileState.Revealed && t.AdjacentMines != 0)
                    .Where(t => _gameBoard.GetNeighbors(t).Any(n => n.State != TileState.Revealed && !n.IsFlagged));
        }

        public List<Tile> GetBlankTilesWithNeighbors()
        {
            return _gameBoard.Tiles
                    .Where(t => t.State != TileState.Revealed && !t.IsFlagged)
                    .Where(t => _gameBoard.GetNeighbors(t).Any(n => n.State == TileState.Revealed || n.IsFlagged))
                    .ToList();
        }
    }
}
