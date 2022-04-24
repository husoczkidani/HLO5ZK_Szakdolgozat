using MinesweeperWithSolver.Commands;
using MinesweeperWithSolver.Enums;
using MinesweeperWithSolver.Models;
using MinesweeperWithSolver.State;
using System.Windows.Input;

namespace MinesweeperWithSolver.ViewModels
{
    public class SimulationViewModel : BaseViewModel
    {
        private static string desc = "-search for obvious mine tiles and flag them\n" +
                                "-search for obvious number tiles and reveal them\n" +
                                "-repeat the first two step, until there is no obvious\n mine or number tiles\n";
        //combo box 
        public SolverType[] PossibleSolverTypes => new SolverType[] {
            SolverType.SPSRT,
            SolverType.SPSRNT,
            SolverType.SPSRCT,
            SolverType.SPS
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

        private SolverType _selectedSolver = SolverType.SPSRT;
        public SolverType SelectedSolver
        {
            get => _selectedSolver;
            set
            {
                _selectedSolver = value;
                OnPropertyChanged(nameof(SelectedSolver));
                
                switch (SelectedSolver)
                {
                    case SolverType.SPSRT:
                        SolverDesc = desc + "-guess a random blank tile, and start over again";
                        break;
                    case SolverType.SPSRNT:
                        SolverDesc = desc + "-guess a random blank tile that has a revealed\n number neighbor, and start over again";
                        break;
                    case SolverType.SPSRCT:
                        SolverDesc = desc + "-guess a random blank corner tile,\n" +
                                            " if there is no available corner tile, \n" +
                                            "than guess a random blank tile and start over again";
                        break;
                    case SolverType.SPS:
                        SolverDesc = desc + "- set the game as failed";
                        break;
                }
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

        private string _solverDesc = desc + "-guess a random blank tile, and start over again";
        public string SolverDesc
        {
            get => _solverDesc;
            set
            {
                _solverDesc = value;
                OnPropertyChanged(nameof(SolverDesc));
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
