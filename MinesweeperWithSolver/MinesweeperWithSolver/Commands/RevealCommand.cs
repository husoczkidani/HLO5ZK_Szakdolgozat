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
using MinesweeperWithSolver.Data.Services.DataService;
using MinesweeperWithSolver.Data.Entities;

namespace MinesweeperWithSolver.Commands
{
    public class RevealCommand : ICommand
    {
        private readonly GameBoardViewModel _gameBoardViewModel;
        private readonly GameBoard _gameBoard;
        private readonly IDataService<PlayedGame> _dataService;

        public RevealCommand(
            GameBoardViewModel gameBoardViewModel, 
            GameBoard gameBoard,
            IDataService<PlayedGame> dataService)
        {
            _gameBoardViewModel = gameBoardViewModel;
            _gameBoard = gameBoard;
            _dataService = dataService;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _gameBoard.Status == GameStatus.Idle
                   || _gameBoard.Status == GameStatus.InProgress;
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

            if(_gameBoard.Status == GameStatus.Finished)
            {
                
            }
        }
    }
}
