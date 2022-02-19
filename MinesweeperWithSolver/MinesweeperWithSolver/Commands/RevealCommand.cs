using MinesweeperWithSolver.Models;
using MinesweeperWithSolver.State;
using MinesweeperWithSolver.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Linq;

namespace MinesweeperWithSolver.Commands
{
    public class RevealCommand : ICommand
    {
        private readonly GameBoardViewModel _gameBoardViewModel;
        private readonly GameBoard _gameBoard;
        private readonly IRenavigator _endScreenRenavigator;

        public RevealCommand(GameBoardViewModel gameBoardViewModel, GameBoard gameBoard, IRenavigator endScreenRenavigator)
        {
            _gameBoardViewModel = gameBoardViewModel;
            _gameBoard = gameBoard;
            _endScreenRenavigator = endScreenRenavigator;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Tile parameterTile = (Tile)parameter;
            var tile = _gameBoard.Tiles
                .Where(t => t.tileID == parameterTile.tileID)
                .First();
            tile.AdjacentMines = 5;
        }
    }
}
