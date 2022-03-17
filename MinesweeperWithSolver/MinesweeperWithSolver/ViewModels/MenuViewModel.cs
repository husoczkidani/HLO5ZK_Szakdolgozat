using MinesweeperWithSolver.Commands;
using MinesweeperWithSolver.Models;
using MinesweeperWithSolver.State;
using System.Windows.Input;

namespace MinesweeperWithSolver.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private string _playername;
        public string Playername
        {
            get
            {
                return _playername;
            }
            set
            {
                
                _playername = value;
                OnPropertyChanged(nameof(Playername));
            }
        }

        private int _difficulty;
        public int Difficulty { 
            get 
            { 
                return _difficulty; 
            } 
            set 
            {
                _difficulty = value;
                OnPropertyChanged(nameof(Difficulty));
            }
        }

        public ICommand StartGameCommand { get; }
        public ICommand StartSimulationCommand { get; }
        public ICommand ViewLeaderboardCommand { get; }

        public MenuViewModel(
            IRenavigator gameBoardRenavigator, 
            IRenavigator simulationRenavigator,
            IRenavigator leaderBoardRenavigator, 
            GameBoard gameBoard)
        {
            StartGameCommand = new StartGameCommand(this,gameBoardRenavigator, gameBoard);
            StartSimulationCommand = new RenavigateCommand(simulationRenavigator);
            ViewLeaderboardCommand = new RenavigateCommand(leaderBoardRenavigator);
        }

    }
}
