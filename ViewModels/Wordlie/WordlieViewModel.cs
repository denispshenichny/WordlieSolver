using System.Collections.ObjectModel;
using Prism.Events;
using Prism.Mvvm;
using WordlieSolver.Shared;

namespace WordlieSolver.ViewModels.Wordlie
{
    public class WordlieViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

        public WordlieViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            eventAggregator.GetEvent<WordSelectedEvent>().Subscribe(OnWordSelected);
            eventAggregator.GetEvent<RestartEvent>().Subscribe(OnRestart);
            Words = new ObservableCollection<WordViewModel> { new(_eventAggregator) };
        }

        public ObservableCollection<WordViewModel> Words { get; }

        private void OnWordSelected(string word)
        {
            Words[^1].SetWord(word);
            Words.Add(new WordViewModel(_eventAggregator));
        }

        private void OnRestart()
        {
            Words.Clear();
            Words.Add(new WordViewModel(_eventAggregator));
        }
    }
}
