using Microsoft.Extensions.DependencyInjection;
using MinesweeperWithSolver.Data;
using MinesweeperWithSolver.Data.Entities;
using MinesweeperWithSolver.Data.Services.DataService;
using MinesweeperWithSolver.Models;
using MinesweeperWithSolver.State;
using MinesweeperWithSolver.ViewModels;
using MinesweeperWithSolver.ViewModels.Factories;
using System;
using System.Windows;

namespace MinesweeperWithSolver
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IServiceProvider serviceProvider = CreateServiceProvider();
            Window window = serviceProvider.GetRequiredService<MainWindow>();
            window.Show();
        }
        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<DatabaseContext, DatabaseContext>();
            services.AddSingleton<IRootViewModelFactory, RootViewModelFactory>();
            services.AddSingleton<INavigator, Navigator>();
            services.AddSingleton<Solver, Solver>();
            services.AddSingleton<GameBoard, GameBoard>();
            services.AddSingleton<Tile, Tile>();

            services.AddSingleton<IDataService<BaseTable>, GenericDataService<BaseTable>>();
            services.AddSingleton<IDataService<PlayedGame>, GenericDataService<PlayedGame>>();
            services.AddSingleton<IDataService<Simulation>, GenericDataService<Simulation>>();

            services.AddSingleton<CreateViewModel<MenuViewModel>>(s =>
            {
                return () => new MenuViewModel(
                    new ViewModelFactoryRenavigator<GameBoardViewModel>(
                        s.GetRequiredService<INavigator>(),
                        s.GetRequiredService<CreateViewModel<GameBoardViewModel>>()),
                    new ViewModelFactoryRenavigator<SimulationViewModel>(
                        s.GetRequiredService<INavigator>(),
                        s.GetRequiredService<CreateViewModel<SimulationViewModel>>()),
                    new ViewModelFactoryRenavigator<LeaderBoardViewModel>(
                        s.GetRequiredService<INavigator>(),
                        s.GetRequiredService<CreateViewModel<LeaderBoardViewModel>>()),
                    s.GetRequiredService<GameBoard>());
            });

            services.AddSingleton<CreateViewModel<GameBoardViewModel>>(s =>
            {
                return () => new GameBoardViewModel(
                    new ViewModelFactoryRenavigator<MenuViewModel>(
                        s.GetRequiredService<INavigator>(),
                        s.GetRequiredService<CreateViewModel<MenuViewModel>>()),
                    s.GetRequiredService<GameBoard>(),
                    s.GetRequiredService<Solver>());
            });

            services.AddSingleton<CreateViewModel<LeaderBoardViewModel>>(s =>
            {
                return () => new LeaderBoardViewModel();
            });

            services.AddSingleton<CreateViewModel<PrevSimulationsViewModel>>(s =>
            {
                return () => new PrevSimulationsViewModel();
            });

            services.AddSingleton<CreateViewModel<SimulationViewModel>>(s =>
            {
                return () => new SimulationViewModel(
                    new ViewModelFactoryRenavigator<MenuViewModel>(
                        s.GetRequiredService<INavigator>(),
                        s.GetRequiredService<CreateViewModel<MenuViewModel>>()),
                    new ViewModelFactoryRenavigator<PrevSimulationsViewModel>(
                        s.GetRequiredService<INavigator>(),
                        s.GetRequiredService<CreateViewModel<PrevSimulationsViewModel>>()),
                    s.GetRequiredService<Solver>()
                    );
            });

            services.AddScoped<MainWindowViewModel>();
            services.AddScoped<MainWindow>(s => new MainWindow(s.GetRequiredService<MainWindowViewModel>()));

            return services.BuildServiceProvider();
        }
    }
    
}
