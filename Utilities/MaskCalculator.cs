using System.Collections.Generic;
using System.Linq;
using WordlieSolver.Shared;
using WordlieSolver.ViewModels.Wordlie;

namespace WordlieSolver.Utilities
{
    public class MaskCalculator : IMaskCalculator
    {
        private class StatedLetter : ILetter
        {
            public StatedLetter(char character, LetterState state)
            {
                Character = character;
                State = state;
            }

            public char Character { get; }
            public LetterState State { get; }
        }

        private readonly HashSet<char> _missingLetters = new();
        private readonly Dictionary<char, HashSet<int>> _guessedLetters = new();

        public void PushMask(IEnumerable<ILetter> word)
        {
            int i = 0;
            foreach (ILetter letter in word)
            {
                switch (letter.State)
                {
                    case LetterState.Missed: 
                        if (!_guessedLetters.ContainsKey(letter.Character))
                            _missingLetters.Add(letter.Character); 
                        break;
                    case LetterState.Guessed:
                        PushGuessedLetter(letter.Character, i);
                        break;
                    case LetterState.WrongPlace:
                        PushWrongPlacedLetter(letter.Character, i);
                        break;
                }
                i++;
            }
        }

        private void PushGuessedLetter(char letter, int index)
        {
            if (_missingLetters.Contains(letter))
                _missingLetters.Remove(letter);

            _guessedLetters[letter] = new HashSet<int> { index };
        }

        private void PushWrongPlacedLetter(char letter, int index)
        {
            if (_missingLetters.Contains(letter))
                _missingLetters.Remove(letter);

            HashSet<int> guessingLetter = GetGuessingLetter(letter);
            guessingLetter.Remove(index);
            foreach ((char guessed, HashSet<int>? value) in _guessedLetters)
            {
                if (value.Count == 1 && letter != guessed)
                    guessingLetter.Remove(value.ElementAt(0));
            }

            _guessedLetters[letter] = guessingLetter;
        }

        private HashSet<int> GetGuessingLetter(char letter)
        {
            if (_guessedLetters.TryGetValue(letter, out HashSet<int>? result))
                return result;

            return new HashSet<int>(Enumerable.Range(0, Constants.LettersCount));
        }

        public bool IsWordFit(string word)
        {
            if (_missingLetters.Any(word.Contains))
                return false;

            foreach (KeyValuePair<char, HashSet<int>> letter in _guessedLetters)
            {
                bool found = false;
                foreach (int index in letter.Value)
                    if (word[index] == letter.Key)
                        found = true;

                if (!found)
                    return false;
            }

            return true;
        }

        public ILetter[] GetLetters(string word)
        {
            var result = new ILetter[word.Length];
            for (int i = 0; i < result.Length; i++)
            {
                char letter = word[i];
                result[i] = new StatedLetter(letter, GetLetterState(letter));
            }
            return result;
        }

        public void Reset()
        {
            _missingLetters.Clear();
            _guessedLetters.Clear();
        }

        private LetterState GetLetterState(char letter)
        {
            if (!_guessedLetters.ContainsKey(letter))
                return LetterState.Missed;

            return _guessedLetters[letter].Count == 1 ? LetterState.Guessed : LetterState.WrongPlace;
        }
    }
}
