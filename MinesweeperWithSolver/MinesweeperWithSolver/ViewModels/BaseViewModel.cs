using MinesweeperWithSolver.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinesweeperWithSolver.ViewModels
{
    public delegate TViewModel CreateViewModel<TViewModel>() where TViewModel : BaseViewModel;

    public class BaseViewModel : ObservableObject
    {
    }
}
