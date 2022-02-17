using MinesweeperWithSolver.Models;
using MinesweeperWithSolver.State;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinesweeperWithSolver.ViewModels
{
    public class GameBoardViewModel : BaseViewModel
    {
        private readonly GameBoard _gameboard;
        public GameBoardViewModel(IRenavigator endScreenRenavigator, GameBoard gameBoard)
        {
            _gameboard = gameBoard;
        }
    }
}
