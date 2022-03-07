using MinesweeperWithSolver.Models;

namespace MinesweeperWithSolver.ViewModels
{
    public delegate TViewModel CreateViewModel<TViewModel>() where TViewModel : BaseViewModel;

    public class BaseViewModel : ObservableObject
    {
    }
}
