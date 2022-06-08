using Prism.Regions;
using WordlieSolver.Shared;
using WordlieSolver.Views;

namespace WordlieSolver.ViewModels
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel(IRegionManager regionManager)
        {
            regionManager.RegisterViewWithRegion(Constants.WordlieRegion, () => new WordlieControl());
            regionManager.RegisterViewWithRegion(Constants.WordsRegion, () => new WordsControl());
        }
    }
}
