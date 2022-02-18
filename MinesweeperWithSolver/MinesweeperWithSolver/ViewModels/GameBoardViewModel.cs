using Caliburn.Micro;
using MinesweeperWithSolver.Models;
using MinesweeperWithSolver.State;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinesweeperWithSolver.ViewModels
{
    public class GameBoardViewModel : BaseViewModel 
    {
        private int _width;
        public int Width { 
            get => _width;
            set 
            {
                _width = value;
                OnPropertyChanged(nameof(Width));
            } 
        }
        private int _height;
        public int Height
        {
            get => _height;
            set
            {
                _height = value;
                OnPropertyChanged(nameof(Height));
            }
        }

        public BindableCollection<Tile> GameBoardTiles { get; set; }
        public GameBoardViewModel(IRenavigator endScreenRenavigator, GameBoard gameBoard)
        {
            Width = gameBoard.Width * 32;
            Height = gameBoard.Height * 32 + 100;
            GameBoardTiles = new BindableCollection<Tile>(gameBoard.Tiles);
        }
    }
}
