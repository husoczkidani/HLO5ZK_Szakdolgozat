namespace MinesweeperWithSolver.ViewModels
{
    public class SimulationViewModel : BaseViewModel
    {
        private string _selectedDifficulty = "Easy";
        public string SelectedDifficulty
        {
            get 
            {
                return _selectedDifficulty;
            }
            set
            {
                _selectedDifficulty = value;
                OnPropertyChanged(nameof(SelectedDifficulty));
            }
        }

        private string _selectedSolver = "Basic Solver";
        public string SelectedSolver
        {
            get
            {
                return _selectedSolver;
            }
            set
            {
                _selectedSolver = value;
                OnPropertyChanged(nameof(SelectedSolver));
            }
        }
    }
}
