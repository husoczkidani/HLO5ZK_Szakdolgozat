using MinesweeperWithSolver.Models;
using MinesweeperWithSolver.State;
using MinesweeperWithSolver.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MinesweeperWithSolver.Commands
{
    public class StartGameCommand : ICommand
    {
        private readonly MenuViewModel _menuViewModel;
        private readonly IRenavigator _renavigator;
        private readonly GameBoard _gameBoard;

        public StartGameCommand(MenuViewModel menuViewModel, IRenavigator renavigator, GameBoard gameBoard)
        {
            _menuViewModel = menuViewModel;
            _renavigator = renavigator;
            _gameBoard = gameBoard;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var userName = _menuViewModel.Playername ?? "Player";
            var difficulty = _menuViewModel.Difficulty == 0 ? 9 : _menuViewModel.Difficulty;

            _gameBoard.InitializeGameBoard(difficulty, userName);
            _renavigator.Renavigate();
        }
    }
}
