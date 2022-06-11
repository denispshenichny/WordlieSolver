using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using WordlieSolver.ViewModels.Wordlie;

namespace WordlieSolver.Shared
{
    public class LetterStateToBackgroundColorConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(targetType == typeof(Brush)) || !(value is LetterState state))
                return null;

            switch (state)
            {
                case LetterState.None: return Brushes.Transparent;
                case LetterState.Missed: return Brushes.White;
                case LetterState.WrongPlace: return Brushes.LightGray;
                case LetterState.Guessed: return Brushes.DarkOrange;
                default: return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
