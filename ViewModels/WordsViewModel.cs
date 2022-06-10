using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using WordlieSolver.Shared;
using WordlieSolver.Utilities;

namespace WordlieSolver.ViewModels
{
    public class WordsViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMaskCalculator _maskCalculator = new MaskCalculator();
        private string _filterString = string.Empty;

        public WordsViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<WordAppliedEvent>().Subscribe(OnWordApplied);
            _eventAggregator = eventAggregator;
            IEnumerable<string> words = FileReader.ReadAllLines("Resources.russian_nouns.txt")
                .Where(w => w.Length == 5);
            AvailableWords = CollectionViewSource.GetDefaultView(words);
            AvailableWords.Filter = WordsFilter;

            SelectWordCommand = new DelegateCommand<string>(OnSelectWord);
        }

        public string FilterString
        {
            get => _filterString;
            set
            {
                if (SetProperty(ref _filterString, value))
                    UpdateView();
            }
        }

        public ICollectionView AvailableWords { get; }
        public string WordsCount => $"Words fit: {AvailableWords.Cast<string>().Count()}";
        public ICommand SelectWordCommand { get; }

        private void OnSelectWord(string word)
        {
            _eventAggregator.GetEvent<WordSelectedEvent>().Publish(word);
        }

        private bool WordsFilter(object obj)
        {
            if (!(obj is string word))
                return false;

            if (!_maskCalculator.IsWordFit(word))
                return false;

            return string.IsNullOrEmpty(word.Trim()) || word.Contains(FilterString);
        }

        private void UpdateView()
        {
            AvailableWords.Refresh();
            RaisePropertyChanged(nameof(WordsCount));
        }

        private void OnWordApplied(IEnumerable<ILetter> word)
        {
            _maskCalculator.PushMask(word);
            UpdateView();
        }
    }
}
