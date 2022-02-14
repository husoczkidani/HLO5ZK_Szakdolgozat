using MinesweeperWithSolver.Enums;
using MinesweeperWithSolver.State;
using MinesweeperWithSolver.ViewModels.Factories;
using System;
using System.Windows.Input;

namespace MinesweeperWithSolver.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {

        public event EventHandler CanExecuteChanged;

        private INavigator _navigator;
        private readonly IRootViewModelFactory _rootViewModelFactory;

        public UpdateCurrentViewModelCommand(INavigator navigator, IRootViewModelFactory rootViewModelFactory)
        {
            _navigator = navigator;
            _rootViewModelFactory = rootViewModelFactory;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewType)
            {
                ViewType viewType = (ViewType)parameter;

                _navigator.CurrentViewModel = _rootViewModelFactory.CreateViewModel(viewType);
            }
        }

    }
}
