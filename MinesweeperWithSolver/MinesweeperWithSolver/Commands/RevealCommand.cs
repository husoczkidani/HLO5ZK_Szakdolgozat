using MinesweeperWithSolver.Models;
using MinesweeperWithSolver.State;
using MinesweeperWithSolver.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Linq;
using System.Collections.ObjectModel;
using MinesweeperWithSolver.Enums;

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
            return _gameBoard.Status == GameStatus.InProgress;
        }

        public void Execute(object parameter)
        {
            Tile selectedTile = (Tile)parameter;
            if (_gameBoard.IsItFirstMove)
            {
                _gameBoard.FirstMove(selectedTile.X_pos, selectedTile.Y_pos);
            }
            _gameBoard.RevealTile(selectedTile.X_pos, selectedTile.Y_pos);
            _gameBoardViewModel.GameBoardTiles = new ObservableCollection<Tile>(_gameBoard.Tiles);
        }
    }
}
