using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

namespace WordlieSolver.ViewModels.Wordlie
{
    public class LetterViewModel : BindableBase
    {
        private char _character;
        private LetterState _state;

        public LetterViewModel()
        {
            SwitchStateCommand = new DelegateCommand(OnSwitchState, () => IsActive)
                .ObservesProperty(() => IsActive);
        }

        public bool IsActive => Character != default;

        public char Character
        {
            get => _character;
            set
            {
                if (SetProperty(ref _character, value))
                    RaisePropertyChanged(nameof(IsActive));
            }
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
