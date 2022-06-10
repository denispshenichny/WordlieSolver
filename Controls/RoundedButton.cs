using System.Windows;
using System.Windows.Controls;

namespace WordlieSolver.Controls
{
    public class RoundedButton : Button
    {
        public static readonly DependencyProperty HighlightedOpacityProperty = DependencyProperty.Register(
            nameof(HighlightedOpacity),
            typeof(double),
            typeof(RoundedButton),
            new PropertyMetadata(1.0));

        public static readonly DependencyProperty DisabledOpacityProperty = DependencyProperty.Register(
            nameof(DisabledOpacity),
            typeof(double),
            typeof(RoundedButton),
            new PropertyMetadata(1.0));

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            nameof(CornerRadius),
            typeof(CornerRadius),
            typeof(RoundedButton),
            new PropertyMetadata(new CornerRadius()));

        public RoundedButton()
        {
            DefaultStyleKey = typeof(RoundedButton);
        }

        public double HighlightedOpacity
        {
            get => (double)GetValue(HighlightedOpacityProperty);
            set => SetValue(HighlightedOpacityProperty, value);
        }
        public double DisabledOpacity
        {
            get => (double)GetValue(DisabledOpacityProperty);
            set => SetValue(DisabledOpacityProperty, value);
        }
        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
    }
}