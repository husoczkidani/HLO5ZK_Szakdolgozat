﻿using MinesweeperWithSolver.Enums;
using MinesweeperWithSolver.Models;
using MinesweeperWithSolver.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MinesweeperWithSolver.Commands
{
    public class FlagCommand : ICommand
    {
        private readonly GameBoardViewModel _gameBoardViewModel;
        private readonly GameBoard _gameBoard;

        public FlagCommand(GameBoardViewModel gameBoardViewModel, GameBoard gameBoard)
        {
            _gameBoardViewModel = gameBoardViewModel;
            _gameBoard = gameBoard;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _gameBoard.Status == GameStatus.InProgress;
        }

        public void Execute(object parameter)
        {
            Tile selectedTile = (Tile)parameter;
            _gameBoard.FlagTile(selectedTile.X_pos, selectedTile.Y_pos);
            _gameBoardViewModel.GameBoardTiles = new ObservableCollection<Tile>(_gameBoard.Tiles);
        }
    }
}
