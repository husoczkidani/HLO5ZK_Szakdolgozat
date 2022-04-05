using MinesweeperWithSolver.Commands;
using MinesweeperWithSolver.Data.Entities;
using MinesweeperWithSolver.Data.Services.DataService;
using MinesweeperWithSolver.State;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MinesweeperWithSolver.ViewModels
{
    public class LeaderBoardViewModel : BaseViewModel
    {
        private readonly IDataService<PlayedGame> _dataService;

        private string _selectedDifficulty = "0";
        public string SelectedDifficulty
        {
            get => _selectedDifficulty;
            set
            {
                _selectedDifficulty = value;
                OnPropertyChanged(nameof(SelectedDifficulty));
                switch (SelectedDifficulty)
                {
                    case ("0"):
                        TopLeaders = new ObservableCollection<PlayedGame>(GetCollection("Easy"));
                        break;
                    case ("1"):
                        TopLeaders = new ObservableCollection<PlayedGame>(GetCollection("Normal"));
                        break;
                    case ("2"):
                        TopLeaders = new ObservableCollection<PlayedGame>(GetCollection("Hard"));
                        break;
                }
                
            }
        }

        public ObservableCollection<PlayedGame> _topLeaders;
        public ObservableCollection<PlayedGame> TopLeaders
        {
            get => _topLeaders;
            set
            {
                _topLeaders = value;
                OnPropertyChanged(nameof(TopLeaders));
            }
        }

        public ICommand BackCommand { get; }

        public LeaderBoardViewModel(IDataService<PlayedGame> dataService, IRenavigator menuRenavigator)
        {
            _dataService = dataService;
            BackCommand = new RenavigateCommand(menuRenavigator);
            TopLeaders = new ObservableCollection<PlayedGame>(GetCollection("Easy"));
        }
        private List<PlayedGame> GetCollection(string difficulty)
        {
            return _dataService.GetAll()
                .Where(g => g.Difficulty.Equals(difficulty))
                .OrderBy(g => g.Time)
                .Take(10)
                .ToList();
        }
    }

    
}
