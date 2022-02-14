using MinesweeperWithSolver.Commands;
using MinesweeperWithSolver.Enums;
using MinesweeperWithSolver.State;
using MinesweeperWithSolver.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MinesweeperWithSolver.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly INavigator _navigator;
        private readonly IRootViewModelFactory _viewModelFactory;

        public BaseViewModel CurrentViewModel => _navigator.CurrentViewModel;

        public ICommand UpdateCurrentViewModelCommand { get; }

        public MainWindowViewModel(INavigator navigator, IRootViewModelFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;


            _navigator.StateChanged += Navigator_StateChanged;


            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, _viewModelFactory);
            UpdateCurrentViewModelCommand.Execute(ViewType.Menu);
        }
        private void Navigator_StateChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

    }
}
