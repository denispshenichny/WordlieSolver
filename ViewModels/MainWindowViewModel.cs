using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using WordlieSolver.Shared;
using WordlieSolver.Views;

namespace WordlieSolver.ViewModels
{
    public class MainWindowViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        public MainWindowViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            regionManager.RegisterViewWithRegion(Constants.WordlieRegion, () => new WordlieControl());
            regionManager.RegisterViewWithRegion(Constants.WordsRegion, () => new WordsControl());

            _eventAggregator = eventAggregator;

            RestartCommand = new DelegateCommand(OnRestart);
        }

        public ICommand RestartCommand { get; }

        private void OnRestart()
        {
            _eventAggregator.GetEvent<RestartEvent>().Publish();
        }
    }
}
