using System.Collections.ObjectModel;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

namespace WordlieSolver.ViewModels.Wordlie
{
    public class WordViewModel : BindableBase
    {
        private bool _isActive;

        public WordViewModel()
        {
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

        public void SetWord(string word)
        {
            for (int i = 0; i < word.Length; i++)
                Letters[i].Character = word[i];

            IsActive = true;
        }

        private void OnReply()
        {
            IsActive = false;
        }
    }
}
