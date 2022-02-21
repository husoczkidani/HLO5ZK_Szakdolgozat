using Caliburn.Micro;
using MinesweeperWithSolver.Commands;
using MinesweeperWithSolver.Models;
using MinesweeperWithSolver.State;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

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

        public ObservableCollection<Tile> _gameBoardTiles;
        public ObservableCollection<Tile> GameBoardTiles {
            get => _gameBoardTiles;
            set
            {
                _gameBoardTiles = value;
                OnPropertyChanged(nameof(GameBoardTiles));
            }
        }

        public ICommand FlagCommand { get; }
        public ICommand RevealCommand { get; }
        public GameBoardViewModel(IRenavigator endScreenRenavigator, GameBoard gameBoard)
        {
            Width = gameBoard.Width * 30;
            Height = gameBoard.Height * 30 + 100;
            GameBoardTiles = new ObservableCollection<Tile>(gameBoard.Tiles);
            FlagCommand = new FlagCommand(this, gameBoard, endScreenRenavigator);
            RevealCommand = new RevealCommand(this, gameBoard, endScreenRenavigator);
        }
    }
}
