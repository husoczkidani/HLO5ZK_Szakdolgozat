using MinesweeperWithSolver.ViewModels;

namespace MinesweeperWithSolver.State
{
    public class ViewModelFactoryRenavigator<TViewModel> : IRenavigator where TViewModel : BaseViewModel
    {
        private readonly INavigator _navigator;
        private readonly CreateViewModel<TViewModel> _createViewModel;

        public ViewModelFactoryRenavigator( INavigator navigator, CreateViewModel<TViewModel> createViewModel)
        {
            _navigator = navigator;
            _createViewModel = createViewModel;
        }

        public void Renavigate()
        {
            _navigator.CurrentViewModel = _createViewModel();
        }
    }
}
