using MinesweeperWithSolver.Models;
using MinesweeperWithSolver.ViewModels;
using System;

namespace MinesweeperWithSolver.State
{
    public class Navigator : ObservableObject, INavigator
    {
        private BaseViewModel _currentViewModel;

        public event Action StateChanged;

        public BaseViewModel CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                _currentViewModel = value;
                StateChanged?.Invoke();
            }
        }
    }
}
