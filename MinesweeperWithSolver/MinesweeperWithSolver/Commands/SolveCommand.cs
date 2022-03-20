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
        private readonly Solver _solver;

        public SolveCommand(GameBoardViewModel gameBoardViewModel, GameBoard gameBoard, Solver solver)
        {
            _gameBoardViewModel = gameBoardViewModel;
            _gameBoard = gameBoard;
            _solver = solver;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _solver.SmartestSolver();
            _gameBoardViewModel.GameBoardTiles = new ObservableCollection<Tile>(_gameBoard.Tiles);
        }
    }
}

