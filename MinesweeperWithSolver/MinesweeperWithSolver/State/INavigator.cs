using MinesweeperWithSolver.ViewModels;
using System;

namespace MinesweeperWithSolver.State
{
    public interface INavigator
    {
        BaseViewModel CurrentViewModel { get; set; }

        event Action StateChanged;
    }
}
