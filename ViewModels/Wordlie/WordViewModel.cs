using System.Collections.ObjectModel;
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
        }

        public ObservableCollection<LetterViewModel> Letters { get; }

        public bool IsActive
        {
            get => _isActive;
            private set => SetProperty(ref _isActive, value);
        }

        public void SetWord(string word)
        {
            for (int i = 0; i < word.Length; i++)
                Letters[i].Character = word[i];

            IsActive = true;
        }
    }
}
