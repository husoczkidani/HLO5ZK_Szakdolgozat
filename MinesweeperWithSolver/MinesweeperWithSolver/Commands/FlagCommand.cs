using MinesweeperWithSolver.Models;
using MinesweeperWithSolver.State;
using MinesweeperWithSolver.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MinesweeperWithSolver.Commands
{
    public class FlagCommand : ICommand
    {
        private readonly GameBoardViewModel _gameBoardViewModel;
        private readonly GameBoard _gameBoard;
        private readonly IRenavigator _endScreenRenavigator;

        public FlagCommand(GameBoardViewModel gameBoardViewModel, GameBoard gameBoard, IRenavigator endScreenRenavigator)
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
            Tile selectedTile = (Tile)parameter;
            var classTile = _gameBoard.Tiles
                .Where(t => t.tileID == selectedTile.tileID)
                .First();
            classTile.Image = "/Resources/Images/two.png";
            _gameBoardViewModel.GameBoardTiles = new ObservableCollection<Tile>(_gameBoard.Tiles);
        }
    }
}
