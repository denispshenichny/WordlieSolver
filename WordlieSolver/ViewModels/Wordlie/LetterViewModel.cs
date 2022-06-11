using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using WordlieSolver.Shared;

namespace WordlieSolver.ViewModels.Wordlie
{
    public class LetterViewModel : BindableBase, ILetter
    {
        private char _character;
        private LetterState _state;
        private bool _isActive;

        public LetterViewModel()
        {
            SwitchStateCommand = new DelegateCommand(OnSwitchState, () => IsActive)
                .ObservesProperty(() => IsActive);
        }

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
        public LetterState State
        {
            get => _state;
            set => SetProperty(ref _state, value);
        }
        public ICommand SwitchStateCommand { get; }

        private void OnSwitchState()
        {
            State = State == LetterState.Guessed ? LetterState.Missed : (LetterState)((int)State + 1);
        }
    }
}
