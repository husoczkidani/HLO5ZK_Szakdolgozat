using MinesweeperWithSolver.Enums;
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
        private readonly Solver _solver;

        public SimulationCommand(SimulationViewModel simulationViewModel, Solver solver)
        {
            _simulationViewModel = simulationViewModel;
            _solver = solver;
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
            SolverType solverType = _simulationViewModel.SelectedSolver;
            _solver.Simulation(difficulty, numberOfGames, solverType);
            _simulationViewModel.GamesPlayed = _solver.GamesPlayed.ToString();
            _simulationViewModel.GamesSolved = _solver.GamesSolved.ToString();
            _simulationViewModel.GamesFailed = _solver.GamesFailed.ToString();
            _simulationViewModel.MinesFlagged = Math.Round(_solver.MinesFlagged, 3).ToString() + "%";
            _simulationViewModel.TilesRevealed = Math.Round(_solver.TilesRevealed, 3).ToString() + "%";
            _simulationViewModel.SolvingTime = $"{_solver.SolvingTime.Minute}:{_solver.SolvingTime.Second}:{Math.Round((double)_solver.SolvingTime.Millisecond, 3)}";
        }
    }
}
