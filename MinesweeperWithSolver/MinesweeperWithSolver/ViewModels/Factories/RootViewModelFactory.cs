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
        private readonly CreateViewModel<SimulationViewModel> _createSimulationVM;
        private readonly CreateViewModel<PrevSimulationsViewModel> _createPrevSimulationsVM;

        public RootViewModelFactory(
            CreateViewModel<MenuViewModel> createMenuVM,
            CreateViewModel<GameBoardViewModel> createGameBoardVM, 
            CreateViewModel<LeaderBoardViewModel> createLeaderBoardVM, 
            CreateViewModel<SimulationViewModel> createSimultaionVM,
            CreateViewModel<PrevSimulationsViewModel> createPrevSimulationsVM)
        {
            _createMenuVM = createMenuVM;
            _createGameBoardVM = createGameBoardVM;
            _createLeaderBoardVM = createLeaderBoardVM;
            _createSimulationVM = createSimultaionVM;
            _createPrevSimulationsVM = createPrevSimulationsVM;
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
                    return _createSimulationVM();
            case ViewType.PrevSimulations:
                    return _createPrevSimulationsVM();
                default:
                    throw new ArgumentException("ViewType is not correct");
            }

        }
    }
}
