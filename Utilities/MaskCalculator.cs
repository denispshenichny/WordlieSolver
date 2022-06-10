using System.Collections.Generic;
using WordlieSolver.Shared;

namespace WordlieSolver.Utilities
{
    public class MaskCalculator : IMaskCalculator
    {
        public void PushMask(IEnumerable<ILetter> word)
        {
        }

        public bool IsWordFit(string word)
        {
            return true;
        }
    }
}
