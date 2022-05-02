using MinesweeperWithSolver.Commands;
using MinesweeperWithSolver.Data.Entities;
using MinesweeperWithSolver.Data.Services.DataService;
using MinesweeperWithSolver.State;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MinesweeperWithSolver.ViewModels
{
    public class PrevSimulationsViewModel : BaseViewModel
    {
        private readonly IDataService<Simulation> _dataService;

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
                        TopLeaders = new ObservableCollection<Simulation>(GetCollection("Easy"));
                        break;
                    case ("1"):
                        TopLeaders = new ObservableCollection<Simulation>(GetCollection("Normal"));
                        break;
                    case ("2"):
                        TopLeaders = new ObservableCollection<Simulation>(GetCollection("Hard"));
                        break;
                }

            }
        }

        public ObservableCollection<Simulation> _topLeaders;
        public ObservableCollection<Simulation> TopLeaders
        {
            get => _topLeaders;
            set
            {
                _topLeaders = value;
                OnPropertyChanged(nameof(TopLeaders));
            }
        }

        public ICommand BackCommand { get; }

        public PrevSimulationsViewModel(IDataService<Simulation> dataService, IRenavigator simulationRenavigator)
        {
            _dataService = dataService;
            BackCommand = new RenavigateCommand(simulationRenavigator);
            TopLeaders = new ObservableCollection<Simulation>(GetCollection("Easy"));
        }
        private List<Simulation> GetCollection(string difficulty)
        {
            return _dataService.GetAll()
                .Where(g => g.Difficulty.Equals(difficulty))
                .OrderByDescending(g => (double)g.GamesSolved/(double)g.GamesPlayed)
                .Take(10)
                .ToList();
            
        }
    }
}
