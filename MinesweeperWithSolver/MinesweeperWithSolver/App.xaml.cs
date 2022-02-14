using Microsoft.Extensions.DependencyInjection;
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

            services.AddSingleton<IRootViewModelFactory, RootViewModelFactory>();
            services.AddSingleton<INavigator, Navigator>();

            services.AddSingleton<CreateViewModel<MenuViewModel>>(s =>
            {
                return () => new MenuViewModel();
            });

            services.AddSingleton<CreateViewModel<GameBoardViewModel>>(s =>
            {
                return () => new GameBoardViewModel();
            });

            services.AddSingleton<CreateViewModel<LeaderBoardViewModel>>(s =>
            {
                return () => new LeaderBoardViewModel();
            });

            services.AddSingleton<CreateViewModel<EndScreenViewModel>>(s =>
            {
                return () => new EndScreenViewModel();
            });

            services.AddScoped<MainWindowViewModel>();
            services.AddScoped<MainWindow>(s => new MainWindow(s.GetRequiredService<MainWindowViewModel>()));

            return services.BuildServiceProvider();
        }
    }
    
}
