using MinesweeperWithSolver.Enums;
using MinesweeperWithSolver.Models;
using MinesweeperWithSolver.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MinesweeperWithSolver.Commands
{
    public class SolveCommand : ICommand
    {
        private readonly GameBoardViewModel _gameBoardViewModel;
        private readonly GameBoard _gameBoard;
        private readonly BasicSolver _basicSolver;

        public SolveCommand(GameBoardViewModel gameBoardViewModel, GameBoard gameBoard, BasicSolver basicSolver)
        {
            _gameBoardViewModel = gameBoardViewModel;
            _gameBoard = gameBoard;
            _basicSolver = basicSolver;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _basicSolver.Solver();
            _gameBoardViewModel.GameBoardTiles = new ObservableCollection<Tile>(_gameBoard.Tiles);
        }
    }
}
