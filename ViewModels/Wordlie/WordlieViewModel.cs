using System.Collections.ObjectModel;
using Prism.Events;
using Prism.Mvvm;
using WordlieSolver.Shared;

namespace WordlieSolver.ViewModels.Wordlie
{
    public class WordlieViewModel : BindableBase
    {
        public WordlieViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<WordSelectedEvent>().Subscribe(OnWordSelected);
            Words = new ObservableCollection<WordViewModel> { new() };
        }

        public ObservableCollection<WordViewModel> Words { get; }

        private void OnWordSelected(string word)
        {
            Words[^1].SetWord(word);
            Words.Add(new WordViewModel());
        }
    }
}
