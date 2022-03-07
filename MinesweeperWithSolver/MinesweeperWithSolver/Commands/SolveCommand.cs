using MinesweeperWithSolver.Enums;
using MinesweeperWithSolver.Models;
using System;
using System.Windows.Input;

namespace MinesweeperWithSolver.Commands
{
    public class SolveCommand : ICommand
    {
        private readonly GameBoard _gameBoard;
        private readonly BasicSolver _basicSolver;

        public SolveCommand(GameBoard gameBoard, BasicSolver basicSolver)
        {
            _gameBoard = gameBoard;
            _basicSolver = basicSolver;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _gameBoard.Status == GameStatus.InProgress;
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
