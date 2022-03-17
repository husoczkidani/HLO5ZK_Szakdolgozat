using MinesweeperWithSolver.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinesweeperWithSolver.ViewModels.Factories
{
    public class RootViewModelFactory : IRootViewModelFactory
    {
        private readonly CreateViewModel<MenuViewModel> _createMenuVM;
        private readonly CreateViewModel<GameBoardViewModel> _createGameBoardVM;
        private readonly CreateViewModel<LeaderBoardViewModel> _createLeaderBoardVM;
        private readonly CreateViewModel<SimulationViewModel> _createSimultaionVM;

        public RootViewModelFactory(
            CreateViewModel<MenuViewModel> createMenuVM,
            CreateViewModel<GameBoardViewModel> createGameBoardVM, 
            CreateViewModel<LeaderBoardViewModel> createLeaderBoardVM, 
            CreateViewModel<SimulationViewModel> createSimultaionVM)
        {
            _createMenuVM = createMenuVM;
            _createGameBoardVM = createGameBoardVM;
            _createLeaderBoardVM = createLeaderBoardVM;
            _createSimultaionVM = createSimultaionVM;
        }

        public BaseViewModel CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Menu:
                    return _createMenuVM();
                case ViewType.GameBoard:
                    return _createGameBoardVM();
                case ViewType.LeaderBoard:
                    return _createLeaderBoardVM();
                case ViewType.Simulation:
                    return _createSimultaionVM();
                default:
                    throw new ArgumentException("ViewType is not correct");
            }

        }
    }
}
