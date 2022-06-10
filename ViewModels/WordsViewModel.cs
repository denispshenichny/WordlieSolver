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
        private bool _isWordSelectable = true;

        public WordsViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<WordAppliedEvent>().Subscribe(OnWordApplied);
            eventAggregator.GetEvent<RestartEvent>().Subscribe(OnRestart);
            _eventAggregator = eventAggregator;
            IEnumerable<string> words = FileReader.ReadAllLines("Resources.russian_nouns.txt")
                .Where(w => w.Length == Constants.LettersCount)
                .Select(w => w.Replace('ё', 'е'));
            AvailableWords = CollectionViewSource.GetDefaultView(words);
            AvailableWords.Filter = WordsFilter;

            SelectWordCommand = new DelegateCommand<string>(OnSelectWord, _ => IsWordSelectable)
                .ObservesCanExecute(() => IsWordSelectable);
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
        public bool IsWordSelectable
        {
            get => _isWordSelectable;
            set => SetProperty(ref _isWordSelectable, value);
        }
        public ICollectionView AvailableWords { get; }
        public string WordsCount => $"Words fit: {AvailableWords.Cast<string>().Count()}";
        public ICommand SelectWordCommand { get; }

        private void OnSelectWord(string word)
        {
            _eventAggregator.GetEvent<WordSelectedEvent>().Publish(_maskCalculator.GetLetters(word));
            FilterString = string.Empty;
            IsWordSelectable = false;
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
            IsWordSelectable = true;
        }

        private void OnRestart()
        {
            _maskCalculator.Reset();
            UpdateView();
            IsWordSelectable = true;
        }
    }
}
