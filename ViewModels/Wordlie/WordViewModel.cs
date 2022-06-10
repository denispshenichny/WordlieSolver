using System.Collections.ObjectModel;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using WordlieSolver.Shared;

namespace WordlieSolver.ViewModels.Wordlie
{
    public class WordViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private bool _isActive;

        public WordViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            var letters = new ObservableCollection<LetterViewModel>();
            for (int i = 0; i < 5; i++)
                letters.Add(new LetterViewModel());

            Letters = letters;

            ReplyCommand = new DelegateCommand(OnReply, () => IsActive)
                .ObservesCanExecute(() => IsActive);
        }

        public ObservableCollection<LetterViewModel> Letters { get; }
        public bool IsActive
        {
            get => _isActive;
            private set
            {
                if (SetProperty(ref _isActive, value))
                    foreach (LetterViewModel letter in Letters)
                        letter.IsActive = value;
            } 
        }
        public ICommand ReplyCommand { get; }

        public void SetWord(ILetter[] word)
        {
            for (int i = 0; i < word.Length; i++)
            {
                Letters[i].Character = word[i].Character;
                Letters[i].State = word[i].State;
            }

            IsActive = true;
        }

        private void OnReply()
        {
            IsActive = false;
            _eventAggregator.GetEvent<WordAppliedEvent>().Publish(Letters);
        }
    }
}
