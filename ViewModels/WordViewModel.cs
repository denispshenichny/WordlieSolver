using System.Collections.ObjectModel;
using Prism.Mvvm;

namespace WordlieSolver.ViewModels
{
    public class WordViewModel : BindableBase
    {
        private bool _isActive;

        public WordViewModel()
        {
            var letters = new ObservableCollection<LetterViewModel>();
            for (int i = 0; i < 5; i++)
                letters.Add(new LetterViewModel() { Character = 'A', IsActive = i % 2 == 0 });

            Letters = letters;
        }

        public ObservableCollection<LetterViewModel> Letters { get; }

        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (SetProperty(ref _isActive, value))
                    foreach (LetterViewModel letter in Letters)
                        letter.IsActive = value;
            }
        }
    }
}
