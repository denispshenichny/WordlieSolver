﻿using System.Collections.Generic;
using System.Linq;
using WordlieSolver.Shared;
using WordlieSolver.ViewModels.Wordlie;

namespace WordlieSolver.Utilities
{
    public class MaskCalculator : IMaskCalculator
    {
        private readonly struct IndexedLetter
        {
            public IndexedLetter(char character, int index)
            {
                Character = character;
                Index = index;
            }

            public char Character { get; }
            public int Index { get; }
        }

        private readonly IList<char> _missingLetters = new List<char>();
        private readonly IList<IndexedLetter> _guessedLetters = new List<IndexedLetter>();
        private readonly IList<IndexedLetter> _wrongPlacedLetters = new List<IndexedLetter>();

        public void PushMask(IEnumerable<ILetter> word)
        {
            int i = 0;
            foreach (ILetter letter in word)
            {
                switch (letter.State)
                {
                    case LetterState.Missed: _missingLetters.Add(letter.Character); break;
                    case LetterState.Guessed: _guessedLetters.Add(new IndexedLetter(letter.Character, i)); break;
                    case LetterState.WrongPlace: _wrongPlacedLetters.Add(new IndexedLetter(letter.Character, i)); break;
                }
                i++;
            }
        }

        public bool IsWordFit(string word)
        {
            if (_missingLetters.Any(word.Contains))
                return false;

            if (_guessedLetters.Any(letter => word[letter.Index] != letter.Character))
                return false;

            return _wrongPlacedLetters.All(letter => word.Contains(letter.Character) && word[letter.Index] != letter.Character);
        }

        public void Reset()
        {
            _missingLetters.Clear();
            _guessedLetters.Clear();
            _wrongPlacedLetters.Clear();
        }
    }
}
