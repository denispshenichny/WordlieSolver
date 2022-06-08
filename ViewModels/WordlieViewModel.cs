using System.Collections.ObjectModel;
using Prism.Mvvm;

namespace WordlieSolver.ViewModels
{
    public class WordlieViewModel : BindableBase
    {
        public WordlieViewModel()
        {
            Words = new ObservableCollection<WordViewModel> { new() };
        }

        public ObservableCollection<WordViewModel> Words { get; }
    }
}
