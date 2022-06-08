using Prism.Mvvm;

namespace WordlieSolver.ViewModels
{
    public class LetterViewModel : BindableBase
    {
        private bool _isActive;
        private char _character;

        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value);
        }

        public char Character
        {
            get => _character;
            set => SetProperty(ref _character, value);
        }
    }
}
