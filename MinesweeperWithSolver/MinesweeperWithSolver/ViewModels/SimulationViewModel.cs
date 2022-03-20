using MinesweeperWithSolver.Commands;
using MinesweeperWithSolver.Enums;
using MinesweeperWithSolver.Models;
using MinesweeperWithSolver.State;
using System.Windows.Input;

namespace MinesweeperWithSolver.ViewModels
{
    public class SimulationViewModel : BaseViewModel
    {
     //combo box   
        public SolverType[] PossibleSolverTypes => new SolverType[] {
            SolverType.StupidSolver,
            SolverType.BasicSolver,
            SolverType.SmartSolver,
            SolverType.SmartestSolver
        };

        //Menu properties
        private string _selectedDifficulty = "0";
        public string SelectedDifficulty
        {
            get => _selectedDifficulty;
            set
            {
                _selectedDifficulty = value;
                OnPropertyChanged(nameof(SelectedDifficulty));
            }
        }

        private SolverType _selectedSolver = SolverType.StupidSolver;
        public SolverType SelectedSolver
        {
            get => _selectedSolver;
            set
            {
                _selectedSolver = value;
                OnPropertyChanged(nameof(SelectedSolver));
            }
        }

        private string _gameNumber = "100";
        public string GameNumber
        {
            get => _gameNumber;
            set
            {
                _gameNumber = value;
                OnPropertyChanged(nameof(GameNumber));
            }
        }

        //Table properties

        private string _gamesPlayed;
        public string GamesPlayed {
            get => _gamesPlayed; 
            set
            {
                _gamesPlayed = value;
                OnPropertyChanged(nameof(GamesPlayed));
            }
        }

        private string _gamesSolved;
        public string GamesSolved
        {
            get => _gamesSolved;
            set
            {
                _gamesSolved = value;
                OnPropertyChanged(nameof(GamesSolved));
            }
        }

        private string _gamesFailed;
        public string GamesFailed
        {
            get => _gamesFailed;
            set
            {
                _gamesFailed = value;
                OnPropertyChanged(nameof(GamesFailed));
            }
        }

        private string _minesFlagged;
        public string MinesFlagged
        {
            get => _minesFlagged;
            set
            {
                _minesFlagged = value;
                OnPropertyChanged(nameof(MinesFlagged));
            }
        }

        private string _tilesRevealed;
        public string TilesRevealed
        {
            get => _tilesRevealed;
            set
            {
                _tilesRevealed = value;
                OnPropertyChanged(nameof(TilesRevealed));
            }
        }

        private string _solvingTime;
        public string SolvingTime
        {
            get => _solvingTime;
            set
            {
                _solvingTime = value;
                OnPropertyChanged(nameof(SolvingTime));
            }
        }

        public ICommand StartSimulationCommand { get; }
        public ICommand ShowPreviousSimulationsCommand { get; }
        public ICommand BackCommand { get; }

        public SimulationViewModel( 
            IRenavigator menuRenavigator, 
            IRenavigator prevSimulations, 
            Solver basicSolver)
        {
            StartSimulationCommand = new SimulationCommand(this, basicSolver);
            ShowPreviousSimulationsCommand = new RenavigateCommand(prevSimulations);
            BackCommand = new RenavigateCommand(menuRenavigator);
        }
    }
}
