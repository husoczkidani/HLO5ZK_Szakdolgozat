using System.Linq;
using System.Data;
using MinesweeperWithSolver.Enums;
using System.Collections.Generic;
using System;

namespace MinesweeperWithSolver.Models
{
    public class Solver
    {
       

        public int GamesPlayed { get; set; }
        public int GamesSolved { get; set; }
        public int GamesFailed { get; set; }
        public double MinesFlagged { get; set; }
        public double TilesRevealed { get; set; }
        public String SolvingTime { get; set; }

        private readonly GameBoard _gameBoard;

        public Solver(GameBoard gameBoard)
        {
            _gameBoard = gameBoard;
        }

        /* Solving steps:
        * - Search for obvious mine tiles, and flag them
        * - Search for obvious number tiles and reveale them
        * - repeate until it can be repeated
        * - Search for bordering blank tiles, and guesses a random tile from them
        * - repeate until the game is over */
        public void SmartSolver()
        {
            while (_gameBoard.Status == GameStatus.InProgress)
            {
                if (!SearchForObviousMines() && !SearchForObviousNumbers())
                {
                    GuessRandomNeighboringTile();
                }
            }
        }

        /* Solving steps:
        * - Search for obvious mine tiles, and flag them
        * - repeate until it can be repeated
        * - Search for obvious number tiles and reveale them
        * - repeate until it can be repeated
        * - Search for bordering blank tiles, and guesses a random tile from them
        * - repeate until the game is over */
        public void SmartestSolver()
        {
            while(_gameBoard.Status == GameStatus.InProgress)
            {
                bool canFlag = true;
                bool canReveal = true;
                while (canFlag)
                {
                    canFlag = SearchForObviousMines();
                }
                canReveal = SearchForObviousNumbers();
                while (canReveal)
                {
                    canReveal = SearchForObviousNumbers();
                }
                canFlag = SearchForObviousMines();
                if (!canFlag && !canReveal && _gameBoard.Status == GameStatus.InProgress)
                {
                    GuessRandomNeighboringTile();
                }
            }
            
        }

        public void Simulation(int difficulty, int numberOfSimulations, SolverType solverType)
        {
            GamesSolved = 0; 
            GamesFailed = 0;
            MinesFlagged = 0;
            TilesRevealed = 0;

            double minesFlagged = 0;
            double tilesRevealed = 0;
            DateTime startingTime = DateTime.Now;

            _gameBoard.InitializeGameBoard(difficulty);
            for (GamesPlayed = 0; GamesPlayed < numberOfSimulations; GamesPlayed++)
            {
                _gameBoard.FirstMove(0, 0);
                _gameBoard.RevealTile(0, 0);
                switch (solverType)
                {
                    case SolverType.StupidSolver:

                        break;
                    case SolverType.BasicSolver:

                        break;
                    case SolverType.SmartSolver:
                        SmartSolver();
                        break;
                    case SolverType.SmartestSolver:
                        SmartestSolver();
                        break;
                }
                if (_gameBoard.Status == GameStatus.Finished) GamesSolved++;
                if (_gameBoard.Status == GameStatus.Failed) GamesFailed++;

                minesFlagged += _gameBoard.Tiles.Where(t => t.IsFlagged == true).Count();
                tilesRevealed += _gameBoard.Tiles.Where(t => t.State == TileState.Revealed).Count();

                _gameBoard.InitializeGameBoard();
            }

            MinesFlagged = minesFlagged / (double)(GamesPlayed * _gameBoard.MineCount)*100;
            TilesRevealed = tilesRevealed / (double)(GamesPlayed * _gameBoard.Tiles.Count())*100;
            TimeSpan span = DateTime.Now.Subtract(startingTime);
            SolvingTime = $"{span.Minutes}:{span.Seconds}:{Math.Round((double)span.Milliseconds,3)}";
        }

        private bool SearchForObviousMines()
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

        private bool SearchForObviousNumbers()
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

        private void GuessRandomTile()
        {
            var blankTiles = _gameBoard.Tiles
                .Where(t => t.State != TileState.Revealed && !t.IsFlagged)
                .ToList();
            Random rand = new Random();
            int guess = rand.Next(0, blankTiles.Count() - 1);
            _gameBoard.RevealTile(blankTiles[guess].X_pos, blankTiles[guess].Y_pos);
        }

        private void GuessRandomNeighboringTile()
        {
            var blankTilesWithNeighbors = _gameBoard.Tiles
                    .Where(t => t.State != TileState.Revealed && !t.IsFlagged)
                    .Where(t => _gameBoard.GetNeighbors(t).Any(n => n.State == TileState.Revealed || n.IsFlagged))
                    .ToList();
            Random rand = new Random();
            int guess = rand.Next(0, blankTilesWithNeighbors.Count() - 1);
            _gameBoard.RevealTile(blankTilesWithNeighbors[guess].X_pos, blankTilesWithNeighbors[guess].Y_pos);

        }

        private IEnumerable<Tile> GetTilesWithBlankNeighbors()
        {
            return _gameBoard.Tiles
                    .Where(t => t.State == TileState.Revealed && t.AdjacentMines != 0)
                    .Where(t => _gameBoard.GetNeighbors(t).Any(n => n.State != TileState.Revealed && !n.IsFlagged));
        }

    }
}
