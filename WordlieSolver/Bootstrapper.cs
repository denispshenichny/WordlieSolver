using System.Windows;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;
using WordlieSolver.ViewModels;
using WordlieSolver.ViewModels.Wordlie;
using WordlieSolver.Views;

namespace WordlieSolver
{
    public class Bootstrapper : PrismBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            ViewModelLocationProvider.Register<MainWindow, MainWindowViewModel>();
            ViewModelLocationProvider.Register<WordlieControl, WordlieViewModel>();
            ViewModelLocationProvider.Register<WordsControl, WordsViewModel>();
        }
    }
}
