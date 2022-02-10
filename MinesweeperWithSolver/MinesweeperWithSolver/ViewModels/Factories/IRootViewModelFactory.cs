using MinesweeperWithSolver.Enums;

namespace MinesweeperWithSolver.ViewModels.Factories
{
    public interface IRootViewModelFactory
    {
        BaseViewModel CreateViewModel(ViewType viewType);
    }
}
