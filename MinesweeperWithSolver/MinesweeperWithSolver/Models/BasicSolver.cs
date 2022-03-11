using System.Linq;
using System.Data;
using MinesweeperWithSolver.Enums;
using System.Collections.Generic;

namespace MinesweeperWithSolver.Models
{
    public class BasicSolver
    {
        private readonly GameBoard _gameBoard;

        public BasicSolver(GameBoard gameBoard)
        {
            _gameBoard = gameBoard;
        }

        public void basicSolver()
        {
            while(_gameBoard.Status == GameStatus.InProgress)
            {
                if(!searchForObviousMines() && !searchForObviousNumbers())
                {
                    break;
                }
            }
            
        }

        //search for revealed tiles that in the neighboring tiles AdjacentMines - IsFlagged tiles = Blank tiles
        public bool searchForObviousMines()
        {
            bool foundMines = false;

            var tilesWithNeighbors = getTilesWithBlankNeighbors();
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

        public bool searchForObviousNumbers()
        {
            bool foundNumbers = false;

            var tilesWithNeighbors = getTilesWithBlankNeighbors();
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

        public void guessRandomNeighboringTile()
        {
            var tilesWithNeighbors = getTilesWithBlankNeighbors();

        }

        public IEnumerable<Tile> getTilesWithBlankNeighbors()
        {
            return _gameBoard.Tiles
                    .Where(t => t.State == TileState.Revealed && t.AdjacentMines != 0)
                    .Where(t => _gameBoard.GetNeighbors(t).Any(n => n.State != TileState.Revealed && !n.IsFlagged));
        }
    }
}
