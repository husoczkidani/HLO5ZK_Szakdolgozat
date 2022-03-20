using MinesweeperWithSolver.Models;
using MinesweeperWithSolver.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MinesweeperWithSolver.Commands
{
    public class SimulationCommand : ICommand
    {
        private readonly SimulationViewModel _simulationViewModel;
        private readonly BasicSolver _basicSolver;

        public SimulationCommand(SimulationViewModel simulationViewModel, BasicSolver basicSolver)
        {
            _simulationViewModel = simulationViewModel;
            _basicSolver = basicSolver;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            int difficulty = Int32.Parse(_simulationViewModel.SelectedDifficulty) + 1;
            int numberOfGames = Int32.Parse(_simulationViewModel.GameNumber);
            _basicSolver.Solver(difficulty, numberOfGames);
            _simulationViewModel.GamesPlayed = _basicSolver.GamesPlayed.ToString();
            _simulationViewModel.GamesSolved = _basicSolver.GamesSolved.ToString();
            _simulationViewModel.GamesFailed = _basicSolver.GamesFailed.ToString();
            _simulationViewModel.MinesFlagged = Math.Round(_basicSolver.MinesFlagged, 3).ToString() + "%";
            _simulationViewModel.TilesRevealed = Math.Round(_basicSolver.TilesRevealed, 3).ToString() + "%";
        }
    }
}
