using WordlieSolver.Shared;
using WordlieSolver.Utilities;
using WordlieSolver.ViewModels.Wordlie;
using Xunit;

namespace WordlieSolver.Tests
{
    public class MaskCalculatorTests
    {
        class FakeLetter : ILetter
        {
            public FakeLetter(char character, LetterState state)
            {
                State = state;
                Character = character;
            }

            public char Character { get; }
            public LetterState State { get; }
        }

        private readonly MaskCalculator _maskCalculator;

        public MaskCalculatorTests()
        {
            _maskCalculator = new MaskCalculator();
        }

        [Fact]
        public void MissedLetters()
        {
            _maskCalculator.PushMask(new ILetter[]
            {
                new FakeLetter('п', LetterState.Missed),
                new FakeLetter('и', LetterState.Missed),
                new FakeLetter('р', LetterState.Missed),
                new FakeLetter('а', LetterState.Missed),
                new FakeLetter('т', LetterState.Missed)
            });

            Assert.False(_maskCalculator.IsWordFit("ссссп"));
            Assert.False(_maskCalculator.IsWordFit("исссс"));
            Assert.False(_maskCalculator.IsWordFit("срссс"));
            Assert.False(_maskCalculator.IsWordFit("ссасс"));
            Assert.False(_maskCalculator.IsWordFit("ссстс"));
            Assert.True(_maskCalculator.IsWordFit("ссссс"));
        }

        [Fact]
        public void WrongPlacedLetters()
        {
            _maskCalculator.PushMask(new ILetter[]
            {
                new FakeLetter('п', LetterState.WrongPlace),
                new FakeLetter('и', LetterState.Missed),
                new FakeLetter('р', LetterState.Missed),
                new FakeLetter('а', LetterState.Missed),
                new FakeLetter('т', LetterState.Missed)
            });

            Assert.False(_maskCalculator.IsWordFit("псссс"));
            Assert.False(_maskCalculator.IsWordFit("ссссс"));
            Assert.True(_maskCalculator.IsWordFit("спссс"));
        }

        [Fact]
        public void GuessedLetters()
        {
            _maskCalculator.PushMask(new ILetter[]
            {
                new FakeLetter('п', LetterState.Guessed),
                new FakeLetter('и', LetterState.Missed),
                new FakeLetter('р', LetterState.Missed),
                new FakeLetter('а', LetterState.Missed),
                new FakeLetter('т', LetterState.Missed)
            });

            Assert.False(_maskCalculator.IsWordFit("спссс"));
            Assert.False(_maskCalculator.IsWordFit("ссссс"));
            Assert.True(_maskCalculator.IsWordFit("псссс"));
        }

        [Fact]
        public void RepeatedLetters_GuessingWord()
        {
            _maskCalculator.PushMask(new ILetter[]
            {
                new FakeLetter('л', LetterState.Guessed),
                new FakeLetter('а', LetterState.Guessed),
                new FakeLetter('с', LetterState.Missed),
                new FakeLetter('к', LetterState.WrongPlace),
                new FakeLetter('а', LetterState.Missed)
            });

            Assert.True(_maskCalculator.IsWordFit("ларек"));
            Assert.False(_maskCalculator.IsWordFit("лааск"));
        }

        [Fact]
        public void RepeatedLetters_TargetWord()
        {
            _maskCalculator.PushMask(new ILetter[]
            {
                new FakeLetter('о', LetterState.Guessed),
                new FakeLetter('т', LetterState.Guessed),
                new FakeLetter('р', LetterState.Guessed),
                new FakeLetter('е', LetterState.Missed),
                new FakeLetter('з', LetterState.Missed)
            });

            Assert.True(_maskCalculator.IsWordFit("отрок"));
        }

        [Fact]
        public void RepeatedLetters_WrongPlaced()
        {
            _maskCalculator.PushMask(new ILetter[]
            {
                new FakeLetter('а', LetterState.WrongPlace),
                new FakeLetter('р', LetterState.WrongPlace),
                new FakeLetter('г', LetterState.Missed),
                new FakeLetter('о', LetterState.Missed),
                new FakeLetter('н', LetterState.Missed)
            });

            Assert.False(_maskCalculator.IsWordFit("абарт"));
            Assert.True(_maskCalculator.IsWordFit("баарт"));
        }

        [Fact]
        public void RepeatedLetters_TwoGuessed()
        {
            _maskCalculator.PushMask(new ILetter[]
            {
                new FakeLetter('к', LetterState.Guessed),
                new FakeLetter('а', LetterState.Guessed),
                new FakeLetter('п', LetterState.Guessed),
                new FakeLetter('п', LetterState.Missed),
                new FakeLetter('а', LetterState.Guessed)
            });

            Assert.False(_maskCalculator.IsWordFit("копна"));
            Assert.True(_maskCalculator.IsWordFit("капча"));
        }
    }
}
