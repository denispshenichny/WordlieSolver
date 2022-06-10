using System.Collections.Generic;
using WordlieSolver.ViewModels.Wordlie;

namespace WordlieSolver.Shared
{
    public interface ILetter
    {
        char Character { get; }
        LetterState State { get; }
    }

    public interface IMaskCalculator
    {
        void PushMask(IEnumerable<ILetter> mask);
        bool IsWordFit(string word);
    }
}
